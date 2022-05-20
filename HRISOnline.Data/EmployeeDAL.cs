using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace HRISOnline.Data
{
    public class EmployeeDAL
    {        

        #region Employee
        public EmpPersonal GetEmployeePersonal(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_personal = new EmpPersonal();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetEmployee";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@whatType", "Personal"));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                byte[] imgBytes = new byte[] { };

                                if (rdr["EmployeePic"] != System.DBNull.Value)
                                {
                                    imgBytes = (byte[])rdr["EmployeePic"];
                                }

                                emp_personal.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                emp_personal.intMstCompany = Convert.ToInt32(rdr["intMstCompany"]);
                                emp_personal.Company = rdr["CompanyCode"].ToString();
                                emp_personal.EmpIdCode = rdr["EmpIdCode"].ToString();
                                emp_personal.FirstName = rdr["FirstName"].ToString();
                                emp_personal.LastName = rdr["LastName"].ToString();
                                emp_personal.MiddleName = rdr["MiddleName"].ToString();
                                emp_personal.ExtensionName = rdr["ExtensionName"].ToString();
                                emp_personal.BirthDay = Convert.ToDateTime(rdr["BirthDay"]);
                                emp_personal.BirthPlace = rdr["BirthPlace"].ToString();
                                emp_personal.Gender = rdr["Gender"].ToString();
                                emp_personal.CivilStatus = rdr["CivilStatus"].ToString();
                                emp_personal.Citizenship = rdr["Citizenship"].ToString();
                                emp_personal.Religion = rdr["Religion"].ToString();
                                emp_personal.Height = rdr["Height"].ToString();
                                emp_personal.Weight = rdr["Weight"].ToString();
                                emp_personal.BloodType = rdr["BloodType"].ToString();
                                emp_personal.HomeAddress = rdr["HomeAddress"].ToString();
                                emp_personal.ZipCode = rdr["ZipCode"].ToString();
                                emp_personal.Telephone = rdr["Telephone"].ToString();
                                emp_personal.CurrentAddress = rdr["CurrentAddress"].ToString();
                                emp_personal.CurrentTelephone = rdr["CurrentTelephone"].ToString();
                                emp_personal.CurrentZipCode = rdr["CurrentZipCode"].ToString();
                                emp_personal.EmailAddress = rdr["EmailAddress"].ToString();
                                emp_personal.Cellphone = rdr["Cellphone"].ToString();
                                emp_personal.HDMFNo = rdr["HDMFNo"].ToString();
                                emp_personal.EYEEId = rdr["EYEEId"].ToString();
                                emp_personal.PHICNo = rdr["PHICNo"].ToString();
                                emp_personal.SSSNo = rdr["SSSNo"].ToString();
                                emp_personal.TIN = rdr["TIN"].ToString();
                                emp_personal.intMstTaxCode = Convert.ToInt32(rdr["intMstTaxCode"]);
                                emp_personal.TaxCode = rdr["TaxCode"].ToString();
                                emp_personal.BankAccountNo = rdr["BankAccountNo"].ToString();
                                emp_personal.EmployeePic = "data:image/png;base64," + Convert.ToBase64String(imgBytes);
                                emp_personal.intMstEmpPersonalOriginal = rdr["intMstEmpPersonalOrigin"].ToString();
                                emp_personal.PersonToNotify = rdr["PersonToNotify"].ToString();
                                emp_personal.Relation = rdr["Relation"].ToString();
                                emp_personal.ContactNum = rdr["ContactNum"].ToString();

                                break;
                            }
                        }
                    }
                }

                return emp_personal;

            }
            catch
            {
                return emp_personal;
            }
            
        }

        public List<EmpChildren> GetEmployeeChildren(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_children = new List<EmpChildren>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblMstEmpChildren WHERE intMstEmpPersonal = @intMstEmpPersonal";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var child = new EmpChildren
                                {
                                    intMstEmpChildren = Convert.ToInt32(rdr["intMstEmpChildren"]),
                                    ChildName = rdr["ChildName"].ToString(),
                                    BirthDate = Convert.ToDateTime(rdr["BirthDate"])
                                };

                                emp_children.Add(child);
                            }
                        }
                    }
                }

                return emp_children;

            }
            catch
            {
                return emp_children;
            }            
        }

        public EmpDTR GetEmployeeDTR(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_dtr = new EmpDTR();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblMstEmpDTR WHERE intMstEmpPersonal = @intMstEmpPersonal";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                emp_dtr.intMstEmpDTR = Convert.ToInt32(rdr["intMstEmpDTR"]);
                                emp_dtr.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                emp_dtr.codeMstBranch = rdr["codeMstBranch"].ToString();
                                emp_dtr.intMstDepartment = Convert.ToInt32(rdr["intMstDepartment"]);
                                emp_dtr.intMstPosition = Convert.ToInt32(rdr["intMstPosition"]);
                                emp_dtr.intMstWorkShift = Convert.ToInt32(rdr["intMstWorkShift"]);
                                emp_dtr.codeMstBranchHome = rdr["codeMstBranchHome"].ToString();
                                emp_dtr.DateHired = Convert.ToDateTime(rdr["DateHired"]);
                                emp_dtr.DateSeparated = Convert.ToDateTime(rdr["DateSeparated"]);
                                emp_dtr.DateProbationary = Convert.ToDateTime(rdr["DateProbationary"]);
                                emp_dtr.intMstWorkStatus = Convert.ToInt32(rdr["intMstWorkStatus"]);
                                emp_dtr.IsSupervisor = Convert.ToBoolean(rdr["IsSupervisor"]);
                                emp_dtr.IsSupervisoryRate = Convert.ToBoolean(rdr["IsSupervisoryRate"]);
                                emp_dtr.intMstEmpPersonalSupervisor = rdr["intMstEmpPersonalSupervisor"].ToString();
                                emp_dtr.MonthlyRate = Convert.ToDouble(rdr["MonthlyRate"]);
                                emp_dtr.DailyRate = ReferenceEquals(rdr["DailyRate"], System.DBNull.Value) ? 0.0 : Convert.ToDouble(rdr["DailyRate"]);
                                emp_dtr.DiscAllowance = Convert.ToDouble(rdr["DiscAllowance"]);
                                emp_dtr.Cola = Convert.ToDouble(rdr["Cola"]);
                                emp_dtr.LateDeduction = Convert.ToBoolean(rdr["LateDeduction"]);
                                emp_dtr.DTRPunches = Convert.ToBoolean(rdr["DTRPunches"]);
                                emp_dtr.Biometrics = Convert.ToBoolean(rdr["Biometrics"]);
                                emp_dtr.HDMFEmployee = Convert.ToDouble(rdr["HDMFEmployee"]);
                                emp_dtr.HDMFEmployer = Convert.ToDouble(rdr["HDMFEmployer"]);
                                emp_dtr.CoopMember = Convert.ToBoolean(rdr["CoopMember"]);
                                emp_dtr.IncludeSSS = Convert.ToBoolean(rdr["IncludeSSS"]);
                                emp_dtr.IncludePHIC = Convert.ToBoolean(rdr["IncludePHIC"]);
                                emp_dtr.IncludeHDMF = Convert.ToBoolean(rdr["IncludeHDMF"]);
                                emp_dtr.IncludeWithTax = Convert.ToBoolean(rdr["IncludeWithTax"]);
                                emp_dtr.IncludeHealthInsurance = Convert.ToBoolean(rdr["WithHealthInsurance"]);
                                emp_dtr.CoopDeposit = Convert.ToDouble(rdr["CoopDeposit"]);
                                emp_dtr.CoopSavings = Convert.ToDouble(rdr["CoopSavings"]);
                                emp_dtr.ReasonForSeparation = rdr["ReasonForSeparation"].ToString();
                                emp_dtr.intBioRegistration = ReferenceEquals(rdr["intBioRegistration"], System.DBNull.Value) ? 0 : Convert.ToInt32(rdr["intBioRegistration"]);

                                break;
                            }
                        }
                    }
                }

                return emp_dtr;

            }
            catch
            {
                return emp_dtr;
            }
            
        }

        private int DBNull()
        {
            throw new NotImplementedException();
        }

        public List<EmpEducation> GetEmployeeEducation(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_educ = new List<EmpEducation>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                     
                        cmd.CommandText = "[HR_GetEmpEducation]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var ed = new EmpEducation
                                {
                                    intMstEmpEducation = Convert.ToInt32(rdr["intMstEmpEducation"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    EduLevel = rdr["EduLevel"].ToString(),
                                    School = rdr["School"].ToString(),
                                    Degree = rdr["Degree"].ToString(),
                                    DateFrom = rdr["DateFrom"].ToString(),
                                    DateTo = rdr["DateTo"].ToString(),
                                    Honors = rdr["Honors"].ToString()
                                };
                                emp_educ.Add(ed);
                            }
                        }
                    }
                }

                return emp_educ;

            }
            catch (Exception ex)
            {
                return emp_educ;
            }            
        }

        public List<EmpEligibility> GetEmployeeEligibility(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_elig = new List<EmpEligibility>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_GetEmpEligibility]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var el = new EmpEligibility
                                {
                                    intMstEmpEligibility = Convert.ToInt32(rdr["intMstEmpEligibility"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    Eligibility = rdr["Eligibility"].ToString(),
                                    DateTaken = rdr["DateTaken"].ToString(),
                                    Place = rdr["Place"].ToString(),
                                    LicenseNumber = rdr["LicenseNumber"].ToString(),
                                    LicenseDate = Convert.ToDateTime(rdr["LicenseDate"]).ToShortDateString(),
                                    ExpiryDate = Convert.ToDateTime(rdr["ExpiryDate"]).ToShortDateString()                  
                                };



                                emp_elig.Add(el);
                            }
                        }
                    }
                }

                return emp_elig;

            }
            catch (Exception ex)
            {
                return emp_elig;
            }            
        }

        //SIAO ADDED
        public List<EmpSemenarsTraining> GetEmployeeTraining(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_training = new List<EmpSemenarsTraining>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_GetEmpTraining]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var train = new EmpSemenarsTraining
                                {
                                    //intMstEmpEligibility = Convert.ToInt32(rdr["intMstEmpEligibility"]),
                                    //intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    //Eligibility = rdr["Eligibility"].ToString(),
                                    //DateTaken = rdr["DateTaken"].ToString(),
                                    //Place = rdr["Place"].ToString(),
                                    //LicenseNumber = rdr["LicenseNumber"].ToString(),
                                    //LicenseDate = Convert.ToDateTime(rdr["LicenseDate"]).ToShortDateString(),
                                    //ExpiryDate = Convert.ToDateTime(rdr["ExpiryDate"]).ToShortDateString()

                                    intMstEmpTraining = Convert.ToInt32(rdr["intMstEmpTraining"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    TrainingTitle = rdr["TrainingSeminar"].ToString(),
                                    DateStart = rdr["DateStart"].ToString(),
                                    strSponsor = rdr["ConductedBy"].ToString(),
                                    Location = rdr["Place"].ToString()

                                };
                                emp_training.Add(train);
                            }
                        }
                    }
                }
                return emp_training;
            }
            catch (Exception ex)
            {
                return emp_training;
            }
        }

        //END OF SIAO ADDED


        public EmpFamily GetEmployeeFamily(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_family = new EmpFamily();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_GetEmpFamily]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                emp_family.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                emp_family.intMstEmpFamily = Convert.ToInt32(rdr["intMstEmpFamily"]);
                                emp_family.SpouseLastname = rdr["SpouseLastname"].ToString();
                                emp_family.SpouseMiddlename = rdr["SpouseMiddlename"].ToString();
                                emp_family.SpouseFirstname = rdr["SpouseFirstname"].ToString();
                                emp_family.SpouseAddress = rdr["SpouseAddress"].ToString();
                                emp_family.SpouseTelephone = rdr["SpouseTelephone"].ToString();
                                emp_family.FatherLastname = rdr["FatherLastname"].ToString();
                                emp_family.FatherMiddlename = rdr["FatherMiddlename"].ToString();
                                emp_family.FatherFirstname = rdr["FatherFirstname"].ToString();
                                emp_family.MotherLastname = rdr["MotherLastname"].ToString();
                                emp_family.MotherMiddlename = rdr["MotherMiddlename"].ToString();
                                emp_family.MotherFirstname = rdr["MotherFirstname"].ToString();
                            }
                        }
                    }
                }

                return emp_family;

            }
            catch (Exception ex)
            {
                return emp_family;
            }            
        }

        public List<EmpWorkExperience> GetEmployeeWorkExperience(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var emp_works = new List<EmpWorkExperience>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_GetEmpWorkExperience]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var we = new EmpWorkExperience
                                {
                                    intMstWorkExperience = Convert.ToInt32(rdr["intMstEmpWorkExperience"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    DateStart = rdr["DateStart"].ToString(),
                                    DateEnd = rdr["DateEnd"].ToString(),
                                    Position = rdr["Position"].ToString(),
                                    OfficeCompany = rdr["OfficeCompany"].ToString(),
                                    MonthlySalary = rdr["MonthlySalary"].ToString(),
                                    //EmploymentStatus = rdr["EmploymentStatus"].ToString(),
                                    ContactNumber = rdr["EmployerContact"].ToString(),
                                    Reason = rdr["Reason"].ToString(),
                                    isGovernment = Convert.ToBoolean(rdr["isGovernment"]),
                                                                      
                                };

                                emp_works.Add(we);
                            }
                        }
                    }
                }

                return emp_works;

            }
            catch (Exception ex)
            {
                return emp_works;
            }
            
        }

        public List<EmpDTRData> GetEmployeeDTRData(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpDTRData>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ViewDTR";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@MstBranch_BranchCode", branchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", DateStart));
                        cmd.Parameters.Add(new SqlParameter("@DateEnd", DateEnd));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var data = new EmpDTRData
                                {
                                    BiometricDate = Convert.ToDateTime(rdr["BiometricDate"]).ToShortDateString(),
                                    BiometricDayName = rdr["BiometricDayName"].ToString(),
                                    WorkShiftName = rdr["WorkShiftName"].ToString(),
                                    DayType = rdr["DayType"].ToString(),
                                    TimeIn1 = rdr["TimeIn1"].ToString(),
                                    TimeIn2 = rdr["TimeIn2"].ToString(),
                                    TimeOut1 = rdr["TimeOut1"].ToString(),
                                    TimeOut2 = rdr["TimeOut2"].ToString(),
                                    LeaveType = rdr["LeaveType"].ToString(),
                                    OTHrs = Convert.ToDouble(rdr["OTHrs"]),
                                    DaysAbsent = Convert.ToDouble(rdr["DaysAbsent"]),
                                    MinutesLate = Convert.ToDouble(rdr["MinutesLate"]),
                                    RegularHrs = Convert.ToDouble(rdr["RegularHrs"]),
                                    MinutesUT = Convert.ToDouble(rdr["MinutesUT"]),
                                    isOfficialBusiness = Convert.ToBoolean(rdr["isOfficialBusiness"])
                                };

                                list.Add(data);
                            }
                        }
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                return list;
            }
            
        }

        public string SaveEmployee(EmpSaving emp, string username)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_AddEditMstEmployee]";
                        //PERSONAL                        
                        cmd.Parameters.Add(new SqlParameter("@pintMstEmpPersonal", emp.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@pintMstCompany", emp.intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@pEmpIdCode", emp.EmpIdCode));
                        cmd.Parameters.Add(new SqlParameter("@pFirstName", emp.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@pMiddleName", emp.MiddleName));
                        cmd.Parameters.Add(new SqlParameter("@pLastName", emp.LastName));
                        cmd.Parameters.Add(new SqlParameter("@pExtensionName", emp.ExtensionName == null ? "" : emp.ExtensionName));
                        cmd.Parameters.Add(new SqlParameter("@pBirthDay", emp.BirthDay));
                        cmd.Parameters.Add(new SqlParameter("@pBirthPlace", emp.BirthPlace));
                        cmd.Parameters.Add(new SqlParameter("@pGender", emp.Gender));
                        cmd.Parameters.Add(new SqlParameter("@pCivilStatus", emp.CivilStatus));
                        cmd.Parameters.Add(new SqlParameter("@pCitizenship", emp.Citizenship == null ? "" : emp.Citizenship));
                        cmd.Parameters.Add(new SqlParameter("@pReligion", emp.Religion == null ? "" : emp.Religion));
                        cmd.Parameters.Add(new SqlParameter("@pHeight", emp.Height));
                        cmd.Parameters.Add(new SqlParameter("@pWeight", emp.Weight));
                        cmd.Parameters.Add(new SqlParameter("@pBloodType", emp.BloodType));
                        cmd.Parameters.Add(new SqlParameter("@pEmailAddress", emp.EmailAddress));
                        cmd.Parameters.Add(new SqlParameter("@pCellphone", emp.Cellphone));
                        cmd.Parameters.Add(new SqlParameter("@pHomeAddress", emp.HomeAddress));
                        cmd.Parameters.Add(new SqlParameter("@pZipCode", emp.ZipCode));
                        cmd.Parameters.Add(new SqlParameter("@pTelephone", emp.Telephone));
                        cmd.Parameters.Add(new SqlParameter("@pCurrentAddress", emp.CurrentAddress));
                        cmd.Parameters.Add(new SqlParameter("@pCurrentZipCode", emp.CurrentZipCode));
                        cmd.Parameters.Add(new SqlParameter("@pCurrentTelephone", emp.CurrentTelephone));
                        cmd.Parameters.Add(new SqlParameter("@pHDMFNo", emp.HDMFNo));
                        cmd.Parameters.Add(new SqlParameter("@pPHICNo", emp.PHICNo));
                        cmd.Parameters.Add(new SqlParameter("@pSSSNo", emp.SSSNo));
                        cmd.Parameters.Add(new SqlParameter("@pEYEEId", emp.EYEEId));
                        cmd.Parameters.Add(new SqlParameter("@pTIN", emp.TIN));
                        cmd.Parameters.Add(new SqlParameter("@pintMstTaxCode", emp.intMstTaxCode));
                        cmd.Parameters.Add(new SqlParameter("@pBankAccountNo", emp.BankAccountNo));

                        //DTR
                        cmd.Parameters.Add(new SqlParameter("@dintMstEmpDTR", emp.intMstEmpDTR));
                        cmd.Parameters.Add(new SqlParameter("@dintMstEmpPersonal", emp.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@dcodeMstBranch", emp.codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@dcodeMstBranchHome", emp.codeMstBranchHome));
                        cmd.Parameters.Add(new SqlParameter("@dintMstDepartment", emp.intMstDepartment));
                        cmd.Parameters.Add(new SqlParameter("@dintMstPosition", emp.intMstPosition));
                        cmd.Parameters.Add(new SqlParameter("@dintMstWorkShift", emp.intMstWorkShift));
                        cmd.Parameters.Add(new SqlParameter("@dDateHired", emp.DateHired));
                        cmd.Parameters.Add(new SqlParameter("@dDateSeparated", emp.DateSeparated));
                        cmd.Parameters.Add(new SqlParameter("@dDateProbationary", emp.DateProbationary));
                        cmd.Parameters.Add(new SqlParameter("@dintMstWorkStatus", emp.intMstWorkStatus));
                        cmd.Parameters.Add(new SqlParameter("@dReasonForSeparation", emp.ReasonForSeparation == null ? "" : emp.ReasonForSeparation));
                        cmd.Parameters.Add(new SqlParameter("@dMonthlyRate", emp.MonthlyRate));
                        cmd.Parameters.Add(new SqlParameter("@dDiscAllowance", emp.DiscAllowance));
                        cmd.Parameters.Add(new SqlParameter("@dCola", emp.Cola));
                        cmd.Parameters.Add(new SqlParameter("@dHDMFEmployee", emp.HDMFEmployee));
                        cmd.Parameters.Add(new SqlParameter("@dIsSupervisoryRate", emp.IsSupervisoryRate));
                        cmd.Parameters.Add(new SqlParameter("@dLateDeduction", emp.LateDeduction));
                        cmd.Parameters.Add(new SqlParameter("@dBiometrics", emp.Biometrics));
                        cmd.Parameters.Add(new SqlParameter("@dIncludeSSS", emp.IncludeSSS));
                        cmd.Parameters.Add(new SqlParameter("@dIncludePHIC", emp.IncludePHIC));
                        cmd.Parameters.Add(new SqlParameter("@dIncludeHDMF", emp.IncludeHDMF));
                        cmd.Parameters.Add(new SqlParameter("@dIncludeWithTax", emp.IncludeWithTax));
                        cmd.Parameters.Add(new SqlParameter("@dCoopMember", emp.CoopMember));
                        cmd.Parameters.Add(new SqlParameter("@dCoopDeposit", emp.CoopDeposit));
                        cmd.Parameters.Add(new SqlParameter("@dCoopSavings", emp.CoopSavings));

                        //FAMILY
                        cmd.Parameters.Add(new SqlParameter("@fintMstEmpPersonal", emp.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@fSpouseFirstname", emp.SpouseFirstname == null ? "" : emp.SpouseFirstname));
                        cmd.Parameters.Add(new SqlParameter("@fSpouseMiddlename", emp.SpouseMiddlename == null ? "" : emp.SpouseMiddlename));
                        cmd.Parameters.Add(new SqlParameter("@fSpouseLastname", emp.SpouseLastname == null ? "" : emp.SpouseLastname));
                        cmd.Parameters.Add(new SqlParameter("@fSpouseTelephone", emp.SpouseTelephone == null ? "" : emp.SpouseTelephone));
                        cmd.Parameters.Add(new SqlParameter("@fSpouseAddress", emp.SpouseAddress == null ? "" : emp.SpouseAddress));
                        cmd.Parameters.Add(new SqlParameter("@fFatherFirstname", emp.FatherFirstname == null ? "" : emp.FatherFirstname));
                        cmd.Parameters.Add(new SqlParameter("@fFatherMiddlename", emp.FatherMiddlename == null ? "" : emp.FatherMiddlename));
                        cmd.Parameters.Add(new SqlParameter("@fFatherLastname", emp.FatherLastname == null ? "" : emp.FatherLastname));
                        cmd.Parameters.Add(new SqlParameter("@fMotherFirstname", emp.MotherFirstname == null ? "" : emp.MotherFirstname));
                        cmd.Parameters.Add(new SqlParameter("@fMotherMiddlename", emp.MotherMiddlename == null ? "" : emp.MotherMiddlename));
                        cmd.Parameters.Add(new SqlParameter("@fMotherLastname", emp.MotherLastname == null ? "" : emp.MotherLastname));

                        cmd.Parameters.Add(new SqlParameter("@AddedBy", username));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                    }
                }

                return strResult;

            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                return strResult;
            }            

        }

        public string SaveEligibility(EmpEligibility elig)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_AddEditMstEligibility]";

                        cmd.Parameters.Add(new SqlParameter("@intMstEmpEligibility", elig.intMstEmpEligibility));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", elig.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@Eligibility", elig.Eligibility));
                        cmd.Parameters.Add(new SqlParameter("@DateTaken", elig.DateTaken));
                        cmd.Parameters.Add(new SqlParameter("@Place", elig.Place));
                        cmd.Parameters.Add(new SqlParameter("@LicenseNumber", elig.LicenseNumber));
                        cmd.Parameters.Add(new SqlParameter("@LicenseDate", elig.LicenseDate));
                        cmd.Parameters.Add(new SqlParameter("@ExpiryDate", elig.ExpiryDate));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                    }
                }

                return strResult;

            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                return strResult;
            }
            
        }

        public string SaveWorkExperience(EmpWorkExperience we)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_AddEditMstWorkExperience]";

                        cmd.Parameters.Add(new SqlParameter("@intMstEmpWorkExperience", we.intMstWorkExperience));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", we.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", we.DateStart));
                        cmd.Parameters.Add(new SqlParameter("@DateEnd", we.DateEnd));
                        cmd.Parameters.Add(new SqlParameter("@Position", we.Position));
                        cmd.Parameters.Add(new SqlParameter("@OfficeCompany", we.OfficeCompany));
                        cmd.Parameters.Add(new SqlParameter("@MonthlySalary", we.MonthlySalary));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentStatus", we.ContactNumber));
                        cmd.Parameters.Add(new SqlParameter("@isGovernment", we.isGovernment));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();

                    }
                }

                return strResult;

            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                return strResult;
            }
            
        }

        public string SaveEducation(EmpEducation educ)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[HR_AddEditMstEducation]";

                        cmd.Parameters.Add(new SqlParameter("@intMstEmpEducation", educ.intMstEmpEducation));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", educ.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@EduLevel", educ.EduLevel));
                        cmd.Parameters.Add(new SqlParameter("@School", educ.School));
                        cmd.Parameters.Add(new SqlParameter("@Degree", educ.Degree));
                        cmd.Parameters.Add(new SqlParameter("@DateGraduated", educ.DateFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateGraduated", educ.DateTo));
                        cmd.Parameters.Add(new SqlParameter("@Honors", educ.Honors));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                    }
                }

                return strResult;

            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                return strResult;
            }

        }

        public string DeleteItem(int id, string module, string username)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Masterfile_MasterfileDelete]";
                        cmd.Parameters.Add(new SqlParameter("@IntId", id));
                        cmd.Parameters.Add(new SqlParameter("@Module", module));
                        cmd.Parameters.Add(new SqlParameter("@AddedBy", username));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                        if (strResult.Contains("Error")) { throw new Exception(strResult); }
                    }
                }

                return strResult;

            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                return strResult;
            }            
        }
        
        public List<NotificationTrans> GetCancelledTransactions(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<NotificationTrans>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetCancelled";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        //cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        //cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        //cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<NotificationTrans>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;

        }

        public List<EmpApprovalNotification> GetEmployeeApprovalNotification(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpApprovalNotification>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetEmployeeNotification]";
                        cmd.CommandTimeout = 180;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpApprovalNotification>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public ApproversName GetEmployeeApproverName(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            ApproversName aprName = new ApproversName();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetApproverName";

                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                aprName.FirstApprover = rdr[0].ToString();
                                aprName.SecondApprover = rdr[1].ToString();
                            }
                        }
                    }
                }

                return aprName;
            }
            catch (Exception)
            {
                return aprName;
            }
        }
        #endregion        

        #region Employee Payroll Detail
        public List<PayrollDetailList> GetDataPayrollDetailList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<PayrollDetailList>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetTrnPayroll]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new PayrollDetailList
                                {
                                    intTrnPayroll = Convert.ToInt32(rdr["intTrnPayroll"]),
                                    Remarks = rdr["Remarks"].ToString(),
                                    PayrollDate = Convert.ToDateTime(rdr["PayrollDate"]).ToShortDateString(),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString()
                                };

                                list.Add(item);
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public PayrollDetailEmployee GetDataPayrollDetailEmployee(int intTrnPayroll, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var result = new PayrollDetailEmployee();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Payroll_GetTrnPayrollDetailEmployee]";
                        cmd.Parameters.Add(new SqlParameter("@intTrnPayroll", intTrnPayroll));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            { 
                                result.intTrnPayroll = Convert.ToInt32(rdr[0]);
                                result.BatchNumber = rdr[1].ToString();
                                result.intMstEmpPersonal = rdr[2].ToString();
                                result.EmployeeName = rdr[3].ToString();
                                result.DailyRate = Convert.ToDouble(rdr[4]);
                                result.MonthlyRate = Convert.ToDouble(rdr[5]);
                                result.NoOfWorkDays = Convert.ToDouble(rdr[6]);
                                result.NoOfRHoliday = Convert.ToDouble(rdr[7]);
                                result.NoOfSHoliday = Convert.ToDouble(rdr[8]);
                                result.NoOfSLVL = Convert.ToDouble(rdr[9]);
                                result.BasicAmount = Convert.ToDouble(rdr[10]);
                                result.BasicCola = Convert.ToDouble(rdr[11]);
                                result.Month13thPay = Convert.ToDouble(rdr[12]);
                                result.OvertimePay = Convert.ToDouble(rdr[13]);
                                result.RHolidayPay = Convert.ToDouble(rdr[14]);
                                result.SHolidayPay = Convert.ToDouble(rdr[15]);
                                result.MonetizePay = Convert.ToDouble(rdr[16]);
                                result.OtherIncome = Convert.ToDouble(rdr[17]);
                                result.AbsenceAmount = Convert.ToDouble(rdr[18]);
                                result.TardyAmount = Convert.ToDouble(rdr[19]);
                                result.UTAmount = Convert.ToDouble(rdr[20]);
                                result.GrossPay = Convert.ToDouble(rdr[21]);
                                result.PHICEmployee = Convert.ToDouble(rdr[22]);
                                result.HDMFEmployee = Convert.ToDouble(rdr[23]);
                                result.SSSEmployee = Convert.ToDouble(rdr[24]);
                                result.WithholdingTax = Convert.ToDouble(rdr[25]);
                                result.OtherDeduction = Convert.ToDouble(rdr[26]);
                                result.NetPay = Convert.ToDouble(rdr[27]);
                                result.VLSLAmount = Convert.ToDouble(rdr[28]);
                                result.NoOfLeaveMonetize = Convert.ToDouble(rdr[29]);
                                result.DaysAbsent = Convert.ToDouble(rdr[30]);
                                result.BasicAllowance = Convert.ToDouble(rdr[31]);
                                result.DiscAllowance = Convert.ToDouble(rdr[32]);
                                result.Cola = Convert.ToDouble(rdr[33]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return result;
        }

        public List<PayrollDetailEmployeeOtherDeduction> GetDataPayrollDetailEmployeeOtherDeduction(int intTrnPayroll, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<PayrollDetailEmployeeOtherDeduction>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Payroll_GetTrnPayrollDetailEmployeeOtherDeduction]";
                        cmd.Parameters.Add(new SqlParameter("@intTrnPayroll", intTrnPayroll));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new PayrollDetailEmployeeOtherDeduction
                                {
                                    intTrnPayrollOtherDeductionDetail = Convert.ToInt32(rdr["intTrnPayrollOtherDeductionDetail"]),
                                    DeductionName = rdr["DeductionName"].ToString(),
                                    Amount = Convert.ToDouble(rdr["Amount"])
                                };

                                list.Add(item);
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public List<PayrollDetailEmployeeOtherIncome> GetDataPayrollDetailEmployeeOtherIncome(int intTrnPayroll, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<PayrollDetailEmployeeOtherIncome>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Payroll_GetTrnPayrollDetailEmployeeOtherIncome]";
                        cmd.Parameters.Add(new SqlParameter("@intTrnPayroll", intTrnPayroll));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new PayrollDetailEmployeeOtherIncome
                                {
                                    intTrnPayrollOtherIncomeDetail = Convert.ToInt32(rdr["intTrnPayrollOtherIncomeDetail"]),
                                    IncomeName = rdr["IncomeName"].ToString(),
                                    Amount = Convert.ToDouble(rdr["Amount"])
                                };

                                list.Add(item);
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public List<AwardsAndBonus> GetDataAwardsAndBonusList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<AwardsAndBonus>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandText = "Online_GetTrnAwardsAndBonus";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<AwardsAndBonus>(dt);
            }
            catch (Exception ex)
            {
                list = null;
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }
        #endregion

        #region Toolbox
        public List<MemorandumToolbox> GetDataMemoToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<MemorandumToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocuments";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<MemorandumToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return list;
        }

        public List<NotificationToolbox> GetNotificationToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<NotificationToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocuments";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<NotificationToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public List<EmpNoticeToolbox> GetEmployeeNoticeToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpNoticeToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocuments";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpNoticeToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public List<EmpAuditToolbox> GetAuditReportToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpAuditToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocuments";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpAuditToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        //FOR DEVIATION 
        // WITHOUT DEVIATION
        public List<EmpAuditToolbox> GetAuditReportWithoutDeviationToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpAuditToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocumentsWithoutDeviation";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpAuditToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }
        //WITH MINOR DIVATION
        public List<EmpAuditToolbox> GetAuditReportWithMinorDeviationToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpAuditToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocumentsWithMinorDeviation";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpAuditToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        //WITH MAJOR DEVIATION
        public List<EmpAuditToolbox> GetAuditReportWithMajorDeviationToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<EmpAuditToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocumentsWithMajorDeviation";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<EmpAuditToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        } 

        //END FOR DEVIATION

        public List<PolicyToolbox> GetPolicyToolbox(string Username, string whatType)
        {
            var dbMgr = new dbManager();
            var list = new List<PolicyToolbox>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_GetDocuments";
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@whatType", whatType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<PolicyToolbox>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public string UpdateDataMemoToOpen(string fileName, string Username, string whatType)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Toolbox_UpdateToOpen";
                        cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@DocType", whatType));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public string AcknowledgeDocument(string fileName, string Username, string whatType)
        {
            var dbMgr = new dbManager();
            string _result = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString())) {
                    using (SqlCommand cmd = new SqlCommand("", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Toolbox_UpdateToAcknowledge]";

                        cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));
                        cmd.Parameters.Add(new SqlParameter("@DocType", whatType));

                        conn.Open();
                        _result = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message.ToString());
            }

            return _result;

        }

        public AnnouncementCount GetAnnouncementCount(string username, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var result = new AnnouncementCount();
            var list = GetEmployeeApprovalNotification(intMstEmpPersonal);

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Toolbox_GetAnnouncementCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.Add(new SqlParameter("@username", username));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result.MemoCount = Convert.ToInt32(rdr["MemoCount"].ToString());
                                result.EmpNoticeCount = Convert.ToInt32(rdr["EmpNoticeCount"]);
                                result.EmpAuditCount = Convert.ToInt32(rdr["EmpAuditCount"]);
                                result.NotificationCount = Convert.ToInt32(rdr["NotificationCount"]);
                                result.PolicyCount = Convert.ToInt32(rdr["PolicyCount"]);
                                result.TotalCount = Convert.ToInt32(rdr["TotalCount"]);

                                result.OTPendingCount = Convert.ToInt32(rdr["OTPendingCount"].ToString());
                                result.LeavePendingCount = Convert.ToInt32(rdr["LeavePendingCount"].ToString());
                                result.UTPendingCount = Convert.ToInt32(rdr["UTPendingCount"].ToString());
                                result.OBPendingCount = Convert.ToInt32(rdr["OBPendingCount"].ToString());
                                result.MPPendingCount = Convert.ToInt32(rdr["MPPendingCount"].ToString());
                                result.OTMealsCount = Convert.ToInt32(rdr["OTMealsCount"].ToString());
                                result.TotalPendingCount = Convert.ToInt32(rdr["TotalPendingCount"].ToString());
                                result.WithOutDeviation = Convert.ToInt32(rdr["WithOutDeviation"].ToString());
                                result.MinorDeviation = Convert.ToInt32(rdr["MinorDeviation"].ToString());
                                result.MajorDeviation = Convert.ToInt32(rdr["MajorDeviation"].ToString());
                                result.PrevAuditReport = Convert.ToInt32(rdr["PrevAudit"].ToString());
                            }
                        }
                    }
                }

                result.EmpApprovalNotification = list.Count;
                return result;
            }
            catch
            {
                return result;
            }
        }
        #endregion

        #region ONE Access
        public List<ObjUserDetails> GetUserDetails(string userName)
        {
            var result = new List<ObjUserDetails>();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionToOneAccess"].ConnectionString;
            using (var con = new SqlConnection(strConnString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("GetUserDetails", con);
                cmd.Parameters.Add(new SqlParameter("@userName", userName));
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result.Add(new ObjUserDetails
                    {
                        userName = rdr[0].ToString(),
                        intMstSysMasterfile = int.Parse(rdr[1].ToString()),
                        sysName = rdr[2].ToString(),
                        sysLink = rdr[3].ToString(),
                        intMstSysRights = int.Parse(rdr[4].ToString()),
                    });
                }
                rdr.Close();
            }
            return result;
        }

        #endregion

        #region Biometrics
        public List<DTRBiometricLogs> GetBiometricLogs(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            var dbMgr = new dbManager();
            var list = new List<DTRBiometricLogs>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[Online_ViewDTRLogs]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@MstBranch_BranchCode", branchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", DateStart));
                        cmd.Parameters.Add(new SqlParameter("@DateEnd", DateEnd));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new DTRBiometricLogs()
                                {
                                    dDate = Convert.ToDateTime(rdr["dDate"]).ToShortDateString(),
                                    LogTimeIn = rdr["LogTimeIn"].ToString(),
                                    LogTimeOut = rdr["LogTimeOut"].ToString()
                                };

                                list.Add(item);
                            }                            
                        }

                    }
                }

                return list;
            }
            catch
            {
                return list;
            }
        }

        public List<DTRBiometrics> GetBiometric(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            var dbMgr = new dbManager();
            var list = new List<DTRBiometrics>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[Online_ViewDTR]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@MstBranch_BranchCode", branchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", DateStart));
                        cmd.Parameters.Add(new SqlParameter("@DateEnd", DateEnd));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new DTRBiometrics()
                                {
                                    intBiometric = int.Parse(rdr["intBiometric"].ToString()),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    DayDate = Convert.ToDateTime(rdr["DayDate"]).ToShortDateString(),
                                    dDayName = rdr["dDayName"].ToString(),
                                    DayType = rdr["DayType"].ToString(),
                                    EmpDayStat = rdr["EmpDayStat"].ToString(),
                                    WorkHour = double.Parse(double.Parse(rdr["WorkHour"].ToString()).ToString("N")),
                                    Tardy = double.Parse(double.Parse(rdr["Tardy"].ToString()).ToString("N")),
                                    Undertime = double.Parse(double.Parse(rdr["Undertime"].ToString()).ToString("N")),
                                    Overtime = double.Parse(double.Parse(rdr["Overtime"].ToString()).ToString("N"))                                    
                                };

                                if (item.EmpDayStat != "REST DAY"){
                                    item.urlDetails = "<a onclick='GetDTRDetails(" + item.DayDate + ");' data-toggle='modal' href='#modal-logs'>Details</a>";
                                    //item.WorkHourStr = Utilities.Functions.ConvertDecimalHoursToWords(item.WorkHour);
                                    item.WorkHourStr = rdr["Duration"].ToString(); //Utilities.Functions.ConvertDecimalHoursToWords(item.WorkHour);
                                }
                                else {
                                    item.WorkHourStr = "";
                                }

                                list.Add(item);
                            }                            
                        }

                    }
                }

                return list;
            }
            catch
            {
                return list;
            }
        }

        public DataTable GetDTReport(DateTime dateStart, DateTime dateEnd, int intMstCompany, string branchCode, string intMstEmpPersonal)
        {
            string query = "[Online_ViewDTR] '" + dateStart + "', '" + dateEnd + "', " + intMstCompany + ", '" + branchCode + "', '" + intMstEmpPersonal + "'";

            return UtilitiesDAL.GetReportSource(query);
            
        }
        #endregion


        public string DocumentReaction(int ReactionType, string ID, string FName, string Username)
        {
            var dbMgr = new dbManager();
            string _result = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[spUploadedMemoReaction]";

                        cmd.Parameters.Add(new SqlParameter("@ReactionType", ReactionType));
                        cmd.Parameters.Add(new SqlParameter("@ID", ID));
                        cmd.Parameters.Add(new SqlParameter("@FName", FName));
                        cmd.Parameters.Add(new SqlParameter("@Username", Username));

                        conn.Open();
                        _result = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return _result;
        }

        public RecentReaction GetRecentReaction(string Filename, string username)
        {
            var dbMgr = new dbManager();
            string _result = string.Empty;
            var result = new RecentReaction();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[spGetRecentReaction]";

                        cmd.Parameters.Add(new SqlParameter("@Filename", Filename));
                        cmd.Parameters.Add(new SqlParameter("@username", username));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result.intReaction = Convert.ToInt32(rdr["intReaction"].ToString());
                                result.strReaction = rdr["strReaction"].ToString();
                                result.IsAllowed = Convert.ToInt32(rdr["IsAllowed"].ToString());
                            }
                        }
                     
         
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }


        }

        //public class GroupTaggingHeaderDLL
        //{

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);

            public DataSet BindDepartmentGroupDDL()
            {
                SqlCommand cmd = new SqlCommand("spGetDapartmentDDL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }


            //var dbMgr = new dbManager();
            //SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString());
          

            //public DataSet BindDepartmentGroupDDL(string CompanyID, string BranchID, string DepartmentID)
            //{
            //    SqlCommand cmd = new SqlCommand("spGetDapartmentDDL", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            //    //cmd.Parameters.AddWithValue("@BranchID", BranchID);
            //    //cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    return ds;
            //}


        //}

    }
}
