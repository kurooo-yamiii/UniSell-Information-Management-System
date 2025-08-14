using Microsoft.AspNetCore.Mvc;
using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.ViewModel;
using CloudinaryDotNet.Actions;
using WebUniform.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebUniform.Controllers
{
    public class SlackController : Controller
    {
        private readonly ISlackRepository _slackrepository;
        private readonly IPhotoService _photoService;
        private readonly IAddressRepository _addressRepository;

        public SlackController(ISlackRepository slackrepository, IPhotoService photoService, IAddressRepository addressRepository)
        {
            _slackrepository = slackrepository;
            _photoService = photoService;
            _addressRepository = addressRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slack> slacks = await _slackrepository.GetAll();
            return View(slacks);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Slack slack = await _slackrepository.GetByIdAsync(id);
            return View(slack);
        }

        public IActionResult Create()
        {
            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);
            var createSlacksViewModel = new SlackCreateModel { AppUserId = userId };

            return View(createSlacksViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SlackCreateModel slackModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(slackModel.Image);
                var slack = new Slack
                {
                    SlackId = 1,
                    Waist = slackModel.Waist,
                    Length = slackModel.Length,
                    Image = result.Url.ToString(),
                    UserId = slackModel.AppUserId,
                    Address = new Address
                    {
                        Street = slackModel.Address.Street,
                        City = slackModel.Address.City,
                        State = slackModel.Address.State
                    }
                };
                _slackrepository.Add(slack);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(slackModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var slack = await _slackrepository.GetByIdAsync(id);
            if (slack == null) return View("Error");

            var SlackVM = new EditSlackViewModel
            {
                Id = slack.Id,
                Waist = slack.Waist,
                Length = slack.Length,
                URL = slack.Image,
                Address = slack.Address,
                AddressId = slack.AddressId
            };

            return View(SlackVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSlackViewModel slackVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Selected Slacks");
                return View("Edit", slackVM);
            }

            var slacks = await _slackrepository.GetByIdAsyncNoTracking(id);
            if (slacks == null)
            {
                ModelState.AddModelError("", "Slack selected is not existing");
                return View(slackVM);
            }

            var existingAddress = await _addressRepository.GetByIdAsync(slackVM.AddressId);
            if (existingAddress == null)
            {
                Console.WriteLine($"Address with ID {slackVM.AddressId} not found");
                ModelState.AddModelError("", "Address not found");
                return View(slackVM);
            }

            string newImageUrl = slacks.Image;
            if (slackVM.Image != null)
            {
                try
                {
                    var fi = new FileInfo(slacks.Image);
                    var publicId = Path.GetFileNameWithoutExtension(fi.Name);
                    await _photoService.DeletePhotoAsync(publicId);

                    var photoResult = await _photoService.AddPhotoAsync(slackVM.Image);
                    newImageUrl = photoResult.Url.ToString();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete or upload photo");
                    Console.WriteLine($"Photo service error: {ex.Message}");
                    return View(slackVM);
                }
            }

          
            existingAddress.Street = slackVM.Address.Street;
            existingAddress.City = slackVM.Address.City;
            existingAddress.State = slackVM.Address.State;
            _addressRepository.Update(existingAddress);

            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);

            var updatedSlack = new Slack
            {
                Id = slacks.Id,
                Waist = slackVM.Waist,
                Length = slackVM.Length,
                Image = newImageUrl,
                AddressId = slackVM.AddressId,
                Address = existingAddress,
                UserId = userId,
                SlackId = 1,
            };

          
            _slackrepository.Update(updatedSlack);

            await _addressRepository.SaveAsync();

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var SlackDetails = await _slackrepository.GetByIdAsync(id);
            if (SlackDetails == null)
            {
                return View("Error");
            }
            return View(SlackDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSlacks(int id)
        {
            var SlackDetails = await _slackrepository.GetByIdAsync(id);
            if (SlackDetails == null)
            {
                return View("Error");
            }
            _slackrepository.Delete(SlackDetails);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Sold")]
        public async Task<IActionResult> MarkAsSold(int id)
        {
            var SlackDetails = await _slackrepository.GetByIdAsync(id);
            if (SlackDetails == null)
            {
                return NotFound();
            }

            SlackDetails.Status = "Sold"; 
            _slackrepository.Update(SlackDetails);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Sale")]
        public async Task<IActionResult> MarkAsSale(int id)
        {
            var SlackDetails = await _slackrepository.GetByIdAsync(id);
            if (SlackDetails == null)
            {
                return NotFound();
            }

            SlackDetails.Status = null; 
             _slackrepository.Update(SlackDetails);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Search(string searchedTerm)
        {
            var results = await _slackrepository.SearchAsync(searchedTerm);
            return View("Index", results);
        }
    }
}
