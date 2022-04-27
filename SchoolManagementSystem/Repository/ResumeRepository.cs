using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using System.IO;
using System.Data.Entity.Validation;
using System.Data.Entity;

using AutoMapper.QueryableExtensions;
using SchoolManagementSystem.ViewModels;
using SchoolManagementSystem.Repository;
using SchoolManagementSystem;

namespace RecruitmentSystemMgt.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        //Db Context
        private readonly SchoolMgtDbEntities _dbContext = new SchoolMgtDbEntities();

        public bool AddCertification(EmployeeCertificationTable certification, int EmployeeResumeID)
        {
            
            try
            {
                int countRecords = 0;
                EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTable.Where(Emp => Emp.EmployeeResumeID == EmployeeResumeID).FirstOrDefault();

                if (personEntity != null && certification != null)
                {
                    personEntity.EmployeeCertificationTable.Add(certification);
                    countRecords = _dbContext.SaveChanges();
                }

                return countRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbEx)
            {

                throw;
            }

        }

        public bool AddLanguage(EmployeeLanguageTable language, int EmployeeResumeID)
        {
            int countRecords = 0;
            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTable.Where(Emp => Emp.EmployeeResumeID == EmployeeResumeID).FirstOrDefault();

            if (personEntity != null && language != null)
            {
                personEntity.EmployeeLanguageTable.Add(language);
                countRecords = _dbContext.SaveChanges();
            }

            return countRecords > 0 ? true : false;
        }

        public string AddOrUpdateEducation(EmployeeEducationTable education, int EmployeeResumeID)
        {
            string msg = string.Empty;

            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTable.Where(Emp => Emp.EmployeeResumeID == EmployeeResumeID).FirstOrDefault();

            if (personEntity != null)
            {
                if (education.EmployeeEducationID > 0)
                {
                    //we will update education entity
                    _dbContext.Entry(education).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    msg = "Education entity has been updated successfully";
                }
                else
                {
                    // we will add new education entity
                    personEntity.EmployeeEducationTable.Add(education);
                    _dbContext.SaveChanges();

                    msg = "Education entity has been Added successfully";
                }
            }

            return msg;
        }

        public string AddOrUpdateExperience(EmployeeWorkExperienceTable workExperience, int EmployeeResumeID)
        {
            string msg = string.Empty;

            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTable.Where(Emp => Emp.EmployeeResumeID == EmployeeResumeID).FirstOrDefault();

            if (personEntity != null)
            {
                if (workExperience.EmployeeWorkExperienceID > 0)
                {
                    //we will update work experience entity
                    _dbContext.Entry(workExperience).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    msg = "Work Experience entity has been updated successfully";
                }
                else
                {
                    // we will add new work experience entity
                    personEntity.EmployeeWorkExperienceTable.Add(workExperience);
                    _dbContext.SaveChanges();

                    msg = "Work Experience entity has been Added successfully";
                }
            }

            return msg;
        }

        public bool AddPersonnalInformation(EmployeeResumeTable person, HttpPostedFileBase file)
        {
            try
            {
                int nbRecords = 0;

                if (person != null)
                {
                    if (file != null)
                    {
                        person.Profil = ConvertToBytes(file);
                    }

                    _dbContext.EmployeeResumeTable.Add(person);
                    nbRecords = _dbContext.SaveChanges();
                }

                return nbRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbEx)
            {

                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        public bool AddSkill(EmployeeSkillTable skill, int EmployeeResumeID)
        {
            int countRecords = 0;
            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTable.Where(Emp => Emp.EmployeeResumeID == EmployeeResumeID).FirstOrDefault();

            if (personEntity != null && skill != null)
            {
                personEntity.EmployeeSkillTable.Add(skill);
                countRecords = _dbContext.SaveChanges();
            }

            return countRecords > 0 ? true : false;

        }

        public IQueryable<EmployeeCertificationTable> GetCertificationsById(int EmployeeResumeID)
        {
            var certificationList = _dbContext.EmployeeCertificationTable.Where(w => w.EmployeeResumeID == EmployeeResumeID);
            return certificationList;
        }

        public IQueryable<EmployeeEducationTable> GetEducationById(int EmployeeResumeID)
        {
            var educationList = _dbContext.EmployeeEducationTable.Where(e => e.EmployeeResumeID == EmployeeResumeID);
            return educationList;
        }

        public int GetIdPerson(int EmployeeID)
        {
            int EmployeeResumeID = _dbContext.EmployeeResumeTable.Where(p => p.EmployeeID == EmployeeID).Select(p => p.EmployeeResumeID).FirstOrDefault();

            return EmployeeResumeID;
        }

        public IQueryable<EmployeeLanguageTable> GetLanguageById(int EmployeeResumeID)
        {
            var languageList = _dbContext.EmployeeLanguageTable.Where(w => w.EmployeeResumeID == EmployeeResumeID);
            return languageList;
        }

        public EmployeeResumeTable GetPersonnalInfo(int EmployeeResumeID)
        {
            return _dbContext.EmployeeResumeTable.Find(EmployeeResumeID);
        }

        public IQueryable<EmployeeSkillTable> GetSkillsById(int EmployeeResumeID)
        {
            var skillsList = _dbContext.EmployeeSkillTable.Where(w => w.EmployeeResumeID == EmployeeResumeID);
            return skillsList;
        }

        public IQueryable<EmployeeWorkExperienceTable> GetWorkExperienceById(int EmployeeResumeID)
        {
            var workExperienceList = _dbContext.EmployeeWorkExperienceTable.Where(w => w.EmployeeWorkExperienceID == EmployeeResumeID);
            return workExperienceList;
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }

}
 