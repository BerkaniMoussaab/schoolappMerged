using AutoMapper;
using SchoolManagementSystem;
using SchoolManagementSystem.Repository;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;


namespace SchoolManagementSystem.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResumeRepository _resumeRepository;
        public ResumeController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckCV(int? id)
        {
            int employeeid = 0;
            if (id == null || id == 0)
            {

                int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeid);
            }
            else
            {
                employeeid = Convert.ToInt32(id);
                Session["EmployeeID"] = employeeid;
            }

            using (SchoolMgtDbEntities db = new SchoolMgtDbEntities())
            {
                var people = db.EmployeeResumeTable.Where(p => p.EmployeeID == employeeid);
                if (people != null)
                {
                    if (people.Count() > 0)
                    {
                        Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson(employeeid);
                        return RedirectToAction("CV", new { id = employeeid });
                    }
                    else
                    {
                        return RedirectToAction("PersonnalInformtion");
                    }
                }
                else
                {
                    return RedirectToAction("PersonnalInformtion");
                }
            }
        }

        public ActionResult ViewCV(int? id)
        {
            return RedirectToAction("CV", new { id = id });
        }


        public ActionResult PersonnalInformtion()
        {
            return View();
        }


        [HttpGet]
        public ActionResult PersonnalInformtion(EmployeeResumeTableMV model)
        {
            //Nationality
            List<SelectListItem> nationality = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Pakistan", Value = "Pakistan", Selected = true},
            };


            //Educational Level
            List<SelectListItem> educationalLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Hight School", Value = "Hight School", Selected = true},
                new SelectListItem { Text = "Diploma", Value = "Diploma"},
                new SelectListItem { Text = "Bachelor's degree", Value = "Bachelor's degree"},
                new SelectListItem { Text = "Master's degree", Value = "Master's degree"},
                new SelectListItem { Text = "Doctorate", Value = "Doctorate"},
            };

            model.ListNationality = nationality;
            model.ListEducationalLevel = educationalLevel;

            return View(model);
        }

        [HttpPost]
        [ActionName("PersonnalInformtion")]
        public ActionResult AddPersonnalInformtion(EmployeeResumeTableMV person)
        {
            var employeeid = 0;
            int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeid);
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            
            person.EmployeeID = employeeid;
            if (ModelState.IsValid)
            {
                //Creating Mapping
                Mapper.Initialize(cfg => cfg.CreateMap<EmployeeResumeTableMV, EmployeeResumeTable>());

                EmployeeResumeTable personEntity = Mapper.Map<EmployeeResumeTable>(person);
                personEntity.EmployeeID = employeeid;
                HttpPostedFileBase file = Request.Files["ImageProfil"];

                bool result = _resumeRepository.AddPersonnalInformation(personEntity, file);

                if (result)
                {

                    Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson(employeeid);
                    return RedirectToAction("Education");
                }
                else
                {
                    ViewBag.Message = "Something Wrong !";
                    return View(person);
                }

            }

            ViewBag.MessageForm = "Please Check your form before submit !";
            return View(person);

        }

        public ActionResult OnlyEducation(int? id)
        {
            Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson((int)id);
            if (Session["EmployeeResumeID"] == null)
            {
                return RedirectToAction("PersonnalInformtion");
            }
            return RedirectToAction("Education");
        }



        [HttpGet]
        public ActionResult Education(EmployeeEducationTableMV education)
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateEducation(EmployeeEducationTableMV education)
        {
            try
            {


                string msg = string.Empty;

                if (education != null)
                {
                    //Creating Mapping
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<EmployeeEducationTableMV, EmployeeEducationTable>());
                    EmployeeEducationTable educationEntity = Mapper.Map<EmployeeEducationTable>(education);
                    int userid = 0;
                    int.TryParse(Convert.ToString(Session["UserID"]), out userid);
                    educationEntity.UserID = userid;

                    int EmployeeResumeID = (int)Session["EmployeeResumeID"];

                    msg = _resumeRepository.AddOrUpdateEducation(educationEntity, EmployeeResumeID);

                }
                else
                {
                    msg = "Please re try the operation";
                }

                return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { data = "Undefined! please Try Again!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public PartialViewResult EducationPartial(EmployeeEducationTableMV education)
        {

            education.ListOfCountry = GetCountries();
            education.ListOfCity = new List<SelectListItem>();
            education.ListOfCity.Add(new SelectListItem() { Text = "KPK", Value = "KPK", Selected = true });
            education.ListOfCity.Add(new SelectListItem() { Text = "Punjab", Value = "Punjab" });
            education.ListOfCity.Add(new SelectListItem() { Text = "Sindh", Value = "Sindh" });
            education.ListOfCity.Add(new SelectListItem() { Text = "Balochistan", Value = "Balochistan" });

            return PartialView("~/Views/Shared/_MyEducation.cshtml", education);
        }

        public ActionResult OnlyWorkExperience(int? id)
        {
            Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson((int)id);
            if (Session["EmployeeResumeID"] == null)
            {
                return RedirectToAction("PersonnalInformtion");
            }
            return RedirectToAction("WorkExperience");
        }


        [HttpGet]
        public ActionResult WorkExperience()
        {
            return View();
        }

        public PartialViewResult WorkExperiencePartial(EmployeeWorkExperienceVM workExperience)
        {
            workExperience.ListeOfCountries = GetCountries();

            return PartialView("~/Views/Shared/_MyWorkExperience.cshtml", workExperience);
        }

        public ActionResult AddOrUpdateExperience(EmployeeWorkExperienceVM workExperience)
        {

            string msg = string.Empty;

            if (workExperience != null)
            {
                //Creating Mapping
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<EmployeeWorkExperienceVM, EmployeeWorkExperienceTable>());
                EmployeeWorkExperienceTable workExperienceEntity = Mapper.Map<EmployeeWorkExperienceTable>(workExperience);

                int EmployeeResumeID = (int)Session["EmployeeResumeID"];
                int userid = 0;
                int.TryParse(Convert.ToString(Session["UserID"]), out userid);
                workExperienceEntity.UserID = userid;
                workExperienceEntity.EmployeeResumeID = EmployeeResumeID;

                msg = _resumeRepository.AddOrUpdateExperience(workExperienceEntity, EmployeeResumeID);

            }
            else
            {
                msg = "Please re try the operation";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlySkiCerfLang(int? id)
        {
            Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson((int)id);
            if (Session["EmployeeResumeID"] == null)
            {
                return RedirectToAction("PersonnalInformtion");
            }
            return RedirectToAction("SkiCerfLang");
        }


        [HttpGet]
        public ActionResult SkiCerfLang()
        {
            return View();
        }

        public PartialViewResult SkillsPartial()
        {
            return PartialView("~/Views/Shared/_MySkills.cshtml");
        }

        public ActionResult AddSkill(EmployeeSkillsVM skill)
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeSkillsVM, EmployeeSkillTable>());
            EmployeeSkillTable skillEntity = Mapper.Map<EmployeeSkillTable>(skill);
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            skillEntity.UserID = userid;
            if (_resumeRepository.AddSkill(skillEntity, EmployeeResumeID))
            {
                msg = "skill added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult CertificationsPartial(EmployeeCertificationVM certification)
        {
            List<SelectListItem> certificationLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Beginner", Value = "Beginner", Selected = true},
                new SelectListItem { Text = "Intermediate", Value = "Intermediate"},
                new SelectListItem { Text = "Advanced", Value = "Advanced"}
            };

            certification.ListOfLevel = certificationLevel;

            return PartialView("~/Views/Shared/_MyCertifications.cshtml", certification);
        }

        public ActionResult AddCertification(EmployeeCertificationVM certification)
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeCertificationVM, EmployeeCertificationTable>());
            EmployeeCertificationTable certificationEntity = Mapper.Map<EmployeeCertificationTable>(certification);

            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            certificationEntity.UserID = userid;

            if (_resumeRepository.AddCertification(certificationEntity, EmployeeResumeID))
            {
                msg = "Certification added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LanguagePartial(EmployeeLanguageVM language)
        {
            List<SelectListItem> languageLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Elementary Proficiency", Value = "Elementary Proficiency", Selected = true},
                new SelectListItem { Text = "LimitedWorking Proficiency", Value = "LimitedWorking Proficiency"},
                new SelectListItem { Text = "Professional working Proficiency", Value = "Professional working Proficiency"},
                new SelectListItem { Text = "Full Professional Proficiency", Value = "Full Professional Proficiency"},
                new SelectListItem { Text = "Native Or Bilingual Proficiency", Value = "Native Or Bilingual Proficiency"}
            };

            language.ListOfProficiency = languageLevel;

            return PartialView("~/Views/Shared/_MyLanguage.cshtml", language);
        }

        public ActionResult AddLanguage(EmployeeLanguageVM language)
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeLanguageVM, EmployeeLanguageTable>());
            EmployeeLanguageTable languageEntity = Mapper.Map<EmployeeLanguageTable>(language);

            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            languageEntity.UserID = userid;

            if (_resumeRepository.AddLanguage(languageEntity, EmployeeResumeID))
            {
                msg = "Language added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CV(int? EmployeeID)
        {
            using (SchoolMgtDbEntities db = new SchoolMgtDbEntities())
            {
                EmployeeID = Convert.ToInt32(Session["EmployeeID"]);
                var person = db.EmployeeResumeTable.Where(p => p.EmployeeID == EmployeeID).FirstOrDefault();
                Session["EmployeeResumeID"] = person.EmployeeResumeID;
                return View();
            }
        }

        public PartialViewResult GetPersonnalInfoPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];
            EmployeeResumeTable person = _resumeRepository.GetPersonnalInfo(EmployeeResumeID);

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeResumeTable, EmployeeResumeTableMV>());
            EmployeeResumeTableMV personVM = Mapper.Map<EmployeeResumeTableMV>(person);

            return PartialView("~/Views/Shared/_MyPersonnalInfo.cshtml", personVM);
        }

        public PartialViewResult GetEducationCVPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeEducationTable, EmployeeEducationTableMV>());
            IQueryable<EmployeeEducationTableMV> educationList = _resumeRepository.GetEducationById(EmployeeResumeID).ProjectTo<EmployeeEducationTableMV>().AsQueryable();

            return PartialView("~/Views/Shared/_MyEducationCV.cshtml", educationList);
        }

        public PartialViewResult WorkExperienceCVPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeWorkExperienceTable, EmployeeWorkExperienceVM>());
            IQueryable<EmployeeWorkExperienceVM> workExperienceList = _resumeRepository.GetWorkExperienceById(EmployeeResumeID).ProjectTo<EmployeeWorkExperienceVM>().AsQueryable();


            return PartialView("~/Views/Shared/_WorkExperienceCV.cshtml", workExperienceList);
        }

        public PartialViewResult SkillsCVPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeSkillTable, EmployeeSkillsVM>());
            IQueryable<EmployeeSkillsVM> skillsList = _resumeRepository.GetSkillsById(EmployeeResumeID).ProjectTo<EmployeeSkillsVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MySkillsCV.cshtml", skillsList);
        }

        public PartialViewResult CertificationsCVPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeCertificationTable, EmployeeCertificationVM>());
            IQueryable<EmployeeCertificationVM> certificationList = _resumeRepository.GetCertificationsById(EmployeeResumeID).ProjectTo<EmployeeCertificationVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MyCertificationCV.cshtml", certificationList);
        }

        public PartialViewResult LanguageCVPartial()
        {
            int EmployeeResumeID = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeLanguageTable, EmployeeLanguageVM>());
            IQueryable<EmployeeLanguageVM> languageList = _resumeRepository.GetLanguageById(EmployeeResumeID).ProjectTo<EmployeeLanguageVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MyLanguageCV.cshtml", languageList);
        }

        public ActionResult GetProfilImage(int id)
        {
            byte[] image = _resumeRepository.GetPersonnalInfo(id).Profil;
            if (image != null)
            {
                return File(image, "image/png");
            }
            else
            {
                return null;
            }

        }

        public ActionResult GetCities(string country)
        {
            List<SelectListItem> listOfCities = new List<SelectListItem>();


            switch (country)
            {
                case "Pakistan":
                    listOfCities.Add(new SelectListItem() { Text = "KPK", Value = "KPK", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Punjab", Value = "Punjab" });
                    listOfCities.Add(new SelectListItem() { Text = "Sindh", Value = "Sindh" });
                    listOfCities.Add(new SelectListItem() { Text = "Balochistan", Value = "Balochistan" });
                    break;

                case "India":
                    listOfCities.Add(new SelectListItem() { Text = "Bombay", Value = "Bombay", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Bangalore", Value = "Bangalore" });
                    listOfCities.Add(new SelectListItem() { Text = "Chennai", Value = "Chennai" });
                    listOfCities.Add(new SelectListItem() { Text = "Hyderabad", Value = "Hyderabad" });
                    break;

                case "Spain":
                    listOfCities.Add(new SelectListItem() { Text = "Barcelone", Value = "Barcelone", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Madrid", Value = "Madrid" });
                    listOfCities.Add(new SelectListItem() { Text = "Valence", Value = "Valence" });
                    listOfCities.Add(new SelectListItem() { Text = "Malaga", Value = "Malaga" });
                    break;

                case "USA":
                    listOfCities.Add(new SelectListItem() { Text = "New York", Value = "New York", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Los Angeles", Value = "Los Angeles" });
                    listOfCities.Add(new SelectListItem() { Text = "San Francisco", Value = "San Francisco" });
                    listOfCities.Add(new SelectListItem() { Text = "Chicago", Value = "Chicago" });
                    break;
            }

            return Json(new { data = listOfCities }, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetCountries()
        {
            List<SelectListItem> listOfCountry = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Pakistan", Value = "Pakistan", Selected = true},
                new SelectListItem() { Text = "Morocco", Value = "Morocco" },
                new SelectListItem() { Text = "India", Value = "India"},
                new SelectListItem() { Text = "Spain", Value = "Spain"},
                new SelectListItem() { Text = "USA", Value = "USA"}
            };

            return listOfCountry;
        }

    }
}