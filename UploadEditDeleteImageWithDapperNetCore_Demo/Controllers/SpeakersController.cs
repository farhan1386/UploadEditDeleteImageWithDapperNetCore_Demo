using Microsoft.AspNetCore.Mvc;
using UploadEditDeleteImageWithDapperNetCore_Demo.Models;
using UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Interface;
using UploadEditDeleteImageWithDapperNetCore_Demo.ViewModels;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        public SpeakersController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var speakers = await unitOfWork.Speakers.Get();
            string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Uploads/" + speakers?.FirstOrDefault().f_speaker_picture;
            ViewBag.SpeakerImage = imageUrl;
            return View(speakers);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var speaker = await unitOfWork.Speakers.Find(id);

            var speakers = new SpeakerViewModel()
            {
                f_uid = speaker.f_uid,
                f_fname = speaker.f_fname,
                f_lname = speaker.f_lname,
                f_gender = speaker.f_gender,
                f_speach_date = speaker.f_speach_date,
                f_speach_time = speaker.f_speach_time,
                f_speach_venue = speaker.f_speach_venue,
                f_existing_photo = speaker.f_speaker_picture
            };
            if (speaker == null)
            {
                return NotFound();
            }
            string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Uploads/" + speakers.f_existing_photo;
            ViewBag.ExistImage = imageUrl;
            return View(speakers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerViewModel model)
        {
            try
            {
                string uniqueFileName = ProcessUploadedFile(model);
                var speaker = new SpeakerModel()
                {
                    f_fname = model.f_fname,
                    f_lname = model.f_lname,
                    f_gender = model.f_gender,
                    f_speach_date = model.f_speach_date,
                    f_speach_time = model.f_speach_time,
                    f_speach_venue = model.f_speach_venue,
                    f_create_date = DateTime.Now,
                    f_speaker_picture = uniqueFileName
                };

                await unitOfWork.Speakers.Add(speaker);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var speaker = await unitOfWork.Speakers.Find(id);
            var speakerExtended = new SpeakerViewModel()
            {
                f_uid = speaker.f_uid,
                f_iid = speaker.f_iid,
                f_fname = speaker.f_fname,
                f_lname = speaker.f_lname,
                f_gender = speaker.f_gender,
                f_speach_date = speaker.f_speach_date,
                f_speach_time = speaker.f_speach_time,
                f_speach_venue = speaker.f_speach_venue,
                f_existing_photo = speaker.f_speaker_picture
            };

            if (speaker == null)
            {
                return NotFound();
            }
            string imageUrl = $"{this.Request.PathBase}" + "/Uploads/" + speaker.f_speaker_picture;
            ViewBag.ExistImage = imageUrl;
            return View(speakerExtended);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SpeakerViewModel model)
        {
            var speaker = await unitOfWork.Speakers.Find(id);
            speaker.f_fname = model.f_fname;
            speaker.f_lname = model.f_lname;
            speaker.f_gender = model.f_gender;
            speaker.f_speach_date = model.f_speach_date;
            speaker.f_speach_time = model.f_speach_time;
            speaker.f_speach_venue = model.f_speach_venue;
            speaker.f_update_date = DateTime.Now;

            if (model.f_speaker_photo != null)
            {
                var CurrentImage = Path.Combine(environment.WebRootPath, "Uploads", speaker.f_speaker_picture);
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }

                speaker.f_speaker_picture = ProcessUploadedFile(model);
            }

            await unitOfWork.Speakers.Update(speaker);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var speaker = await unitOfWork.Speakers.Find(id);
            var speakerExtended = new SpeakerViewModel()
            {
                f_uid = speaker.f_uid,
                f_iid = speaker.f_iid,
                f_fname = speaker.f_fname,
                f_lname = speaker.f_lname,
                f_gender = speaker.f_gender,
                f_speach_date = speaker.f_speach_date,
                f_speach_time = speaker.f_speach_time,
                f_speach_venue = speaker.f_speach_venue,
                f_existing_photo = speaker.f_speaker_picture
            };

            if (speaker == null)
            {
                return NotFound();
            }
            string imageUrl = $"{this.Request.PathBase}" + "/Uploads/" + speaker.f_speaker_picture;
            ViewBag.ExistImage = imageUrl;
            return View(speakerExtended);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var speaker = await unitOfWork.Speakers.Find(id);
            var CurrentImage = Path.Combine(environment.WebRootPath, "Uploads", speaker.f_speaker_picture);
            ViewBag.ExistImage = CurrentImage;
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            await unitOfWork.Speakers.Remove(speaker);
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadedFile(SpeakerViewModel model)
        {
            string uniqueFileName = null;

            if (model.f_speaker_photo != null)
            {
                string path = Path.Combine(environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string uploadsFolder = Path.Combine(environment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.f_speaker_photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.f_speaker_photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
