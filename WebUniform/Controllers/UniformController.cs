using Microsoft.AspNetCore.Mvc;
using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.Repository;
using WebUniform.ViewModel;

namespace WebUniform.Controllers
{
    public class UniformController : Controller
    {
        private readonly IUniformRepository _uniformrepository;
        private readonly IPhotoService _photoService;
        private readonly IAddressRepository _addressRepository;

        public UniformController(IUniformRepository uniformRepository, IPhotoService photoService, IAddressRepository addressRepository)
        {
            _uniformrepository = uniformRepository;
            _photoService = photoService;
            _addressRepository = addressRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Uniform> uniforms = await _uniformrepository.GetAll();
            return View(uniforms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Uniform uniform = await _uniformrepository.GetByIdAsync(id);
            return View(uniform);
        }

        public IActionResult Create()
        {
            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);
            var createUniformViewModel = new UniformCreateModel { AppUserId = userId };

            return View(createUniformViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UniformCreateModel uniformModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(uniformModel.Image);
                var uniform = new Uniform
                {
                    UniformId = 1,
                    Sleeve = uniformModel.Sleeve,
                    Shoulder = uniformModel.Shoulder,
                    Length = uniformModel.Length,
                    Image = result.Url.ToString(),
                    UserId = uniformModel.AppUserId,
                    Address = new Address
                    {
                        Street = uniformModel.Address.Street,
                        City = uniformModel.Address.City,
                        State = uniformModel.Address.State
                    }
                };
                _uniformrepository.Add(uniform);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(uniformModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var uniform = await _uniformrepository.GetByIdAsync(id);
            if (uniform == null) return View("Error");

            var uniformVM = new EditUniformViewModel
            {
                Id = uniform.Id,
                Sleeve = uniform.Sleeve,
                Shoulder = uniform.Shoulder,
                Length = uniform.Length,
                URL = uniform.Image,
                Address = uniform.Address,
                AddressId = uniform.AddressId,
            };

            return View(uniformVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditUniformViewModel uniformVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Selected Uniform");
                return View("Edit", uniformVM);
            }

            var uniform = await _uniformrepository.GetByIdAsyncNoTracking(id);
            if (uniform == null)
            {
                ModelState.AddModelError("", "Uniform selected is not existing");
                return View(uniformVM);
            }

            var existingAddress = await _addressRepository.GetByIdAsync(uniformVM.AddressId);
            if (existingAddress == null)
            {
                Console.WriteLine($"Address with ID {uniformVM.AddressId} not found");
                ModelState.AddModelError("", "Address not found");
                return View(uniformVM);
            }

            string newImageUrl = uniform.Image;
            if (uniformVM.Image != null)
            {
                try
                {
                    var fi = new FileInfo(uniform.Image);
                    var publicId = Path.GetFileNameWithoutExtension(fi.Name);
                    await _photoService.DeletePhotoAsync(publicId);

                    var photoResult = await _photoService.AddPhotoAsync(uniformVM.Image);
                    newImageUrl = photoResult.Url.ToString();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete or upload photo");
                    Console.WriteLine($"Photo service error: {ex.Message}");
                    return View(uniformVM);
                }
            }


            existingAddress.Street = uniformVM.Address.Street;
            existingAddress.City = uniformVM.Address.City;
            existingAddress.State = uniformVM.Address.State;
            _addressRepository.Update(existingAddress);

            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);

            var updateUniform = new Uniform
            {
                Id = uniform.Id,
                Shoulder = uniformVM.Shoulder,
                Sleeve = uniformVM.Sleeve,
                Length = uniformVM.Length,
                Image = newImageUrl,
                AddressId = uniformVM.AddressId,
                Address = existingAddress,
                UserId = userId,
            };


            _uniformrepository.Update(updateUniform);

            await _addressRepository.SaveAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var UniformDetails = await _uniformrepository.GetByIdAsync(id);
            if (UniformDetails == null)
            {
                return View("Error");
            }
            return View(UniformDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUniform(int id)
        {
            var UniformDetails = await _uniformrepository.GetByIdAsync(id);
            if (UniformDetails == null)
            {
                return View("Error");
            }
            _uniformrepository.Delete(UniformDetails);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Sold")]
        public async Task<IActionResult> MarkAsSold(int id)
        {
            var UniformDetails = await _uniformrepository.GetByIdAsync(id);
            if (UniformDetails == null)
            {
                return NotFound();
            }

            UniformDetails.Status = "Sold";
            _uniformrepository.Update(UniformDetails);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Sale")]
        public async Task<IActionResult> MarkAsSale(int id)
        {
            var UniformDetails = await _uniformrepository.GetByIdAsync(id);
            if (UniformDetails == null)
            {
                return NotFound();
            }

            UniformDetails.Status = null;
            _uniformrepository.Update(UniformDetails);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Search(string searchedTerm)
        {
            var results = await _uniformrepository.SearchAsync(searchedTerm);
            return View("Index", results);
        }
    }
}
