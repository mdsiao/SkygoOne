function SaveRecord(myUrl, obj)
{
    var myResult = false;

    //saving part
    $.ajax({
        url: myUrl,
        data: JSON.stringify(obj),
        type: 'POST',
        contentType: 'application/json;',
        dataType: 'json',
        success: function (result) {           

            myResult = result.success;
        }
    });

    return myResult;
}

function GetWorkExperience(url) {
    $('#tbl-we tbody>tr').remove();
    var urlString = url;

    $.get(
        urlString,
        function (data) {

            var arr = JSON.parse(data.obj);
            var x;

            for (x = 0; x < arr.length; x++) {
                var row = "";
                row += '<tr>';
                row += '<td class="center">' + arr[x]['DateStart'] + '</td>';
                row += '<td class="center">' + arr[x]['DateEnd'] + '</td>';
                row += '<td>' + arr[x]['Position'] + '</td>';
                row += '<td>' + arr[x]['OfficeCompany'] + '</td>';
                row += '<td>' + arr[x]['MonthlySalary'] + '</td>';
                row += '<td class="center">' + arr[x]['EmploymentStatus'] + '</td>';
                if (arr[x]['isGovernment'] == true) {
                    row += '<td>YES</td>';
                }
                else {
                    row += '<td>NO</td>';
                }
                row += '<td class="text-center"><a href="javascript:void(0);" onclick="RemoveWE(' + arr[x]["intMstWorkExperience"] + ');">Remove</a></td>';
                row += '</tr>';
                
                $('#tbl-we > tbody:last').append(row);
            }

        });
}

function GetEligibility(url) {
    $('#tbl-elig tbody>tr').remove();
    var urlString = url;

    $.get(
        urlString,
        function (data) {

            var arr = JSON.parse(data.obj);
            var x;

            for (x = 0; x < arr.length; x++) {
                var row = "";
                row += '<tr>';
                row += '<td>' + arr[x]['Eligibility'] + '</td>';
                row += '<td class="center">' + arr[x]['DateTaken'] + '</td>';
                row += '<td>' + arr[x]['Place'] + '</td>';
                row += '<td>' + arr[x]['LicenseNumber'] + '</td>';
                row += '<td class="center">' + arr[x]['LicenseDate'] + '</td>';
                row += '<td class="center">' + arr[x]['ExpiryDate'] + '</td>';                
                row += '<td class="text-center"><a href="javascript:void(0);" onclick="RemoveElig(' + arr[x]["intMstEmpEligibility"] + ');">Remove</a></td>';
                row += '</tr>';

                $('#tbl-elig > tbody:last').append(row);
            }

        });
}

function GetEduc(url) {
    $('#tbl-educ tbody>tr').remove();
    var urlString = url;

    $.get(
        urlString,
        function (data) {

            var arr = JSON.parse(data.obj);
            var x;

            for (x = 0; x < arr.length; x++) {
                var row = "";
                row += '<tr>';
                row += '<td>' + arr[x]['EduLevel'] + '</td>';
                row += '<td class="center">' + arr[x]['School'] + '</td>';
                row += '<td>' + arr[x]['Degree'] + '</td>';
                row += '<td>' + arr[x]['DateGraduated'] + '</td>';
                row += '<td class="center">' + arr[x]['Honors'] + '</td>';                
                row += '<td class="text-center"><a href="javascript:void(0);" onclick="RemoveEduc(' + arr[x]["intMstEmpEducation"] + ');">Remove</a></td>';
                row += '</tr>';

                $('#tbl-educ > tbody:last').append(row);
            }

        });
}

function HideMsg()
{
    $('#msg').removeClass('active');
    $('#msg').addClass('hide');    
}

function ShowMsg()
{
    $('#msg').removeClass('hide');
    $('#msg').addClass('active');
}

function SaveEmployee(url)
{    

    var emp = {
        "intMstEmpPersonal": "", "intMstCompany": "", "EmpIdCode": "", "LastName": "", "FirstName": "", "MiddleName": "", "ExtensionName": "",
        "BirthDay": "", "BirthPlace": "", "Gender": "", "CivilStatus": "", "Citizenship": "", "Religion": "", "Height": "", "Weight": "",
        "BloodType": "", "HomeAddress": "", "ZipCode": "", "Telephone": "", "CurrentAddress": "", "CurrentZipCode": "", "CurrentTelephone": "",
        "EmailAddress": "", "Cellphone": "", "HDMFNo": "", "EYEEId": "", "PHICNo": "", "SSSNo": "",
        "TIN": "", "intMstTaxCode": "", "BankAccountNo": "",
        "intMstEmpDTR": "", "codeMstBranch": "", "intMstDepartment": "", "intMstPosition": "", "intMstWorkShift": "",
        "codeMstBranchHome": "", "DateHired": "", "DateSeparated": "", "DateProbationary": "", "intMstWorkStatus": "", "IsSupervisor": "",
        "IsSupervisoryRate": "", "intMstEmpPersonalSupervisor": "", "MonthlyRate": "", "DailyRate": "", "DiscAllowance": "", "Cola": "",
        "LateDeduction": "", "DTRPunches": "", "Biometrics": "", "HDMFEmployee": "", "HDMFEmployer": "", "CoopMember": "", "IncludeSSS": "",
        "IncludePHIC": "", "IncludeHDMF": "", "IncludeWithTax": "", "IncludeHealthInsurance": "", "CoopDeposit": "", "CoopSavings": "",
        "ReasonForSeparation": "", "intBioRegistration": "",
        "intMstEmpFamily": "", "SpouseFirstname": "", "SpouseLastname": "", "SpouseMiddlename": "",
        "SpouseTelephone": "", "SpouseAddress": "", "FatherLastname": "", "FatherFirstname": "", "FatherMiddlename": "",
        "MotherLastname": "", "MotherFirstname": "", "MotherMiddlename": ""
    };  

    emp.intMstCompany = $('#EmpPersonal_intMstCompany').val();
    emp.EmpIdCode = $('#EmpPersonal_EmpIdCode').val();
    emp.LastName = $('#EmpPersonal_LastName').val();
    emp.FirstName = $('#EmpPersonal_FirstName').val();
    emp.MiddleName = $('#EmpPersonal_MiddleName').val();
    emp.ExtensionName = $('#EmpPersonal_ExtensionName').val();
    emp.BirthDay = $('#EmpPersonal_BirthDay').val();
    emp.BirthPlace = $('#EmpPersonal_BirthPlace').val();
    emp.Gender = $('#EmpPersonal_Gender').val();
    emp.CivilStatus = $('#EmpPersonal_CivilStatus').val();
    emp.Citizenship = $('#EmpPersonal_Citizenship').val();
    emp.Religion = $('#EmpPersonal_Religion').val();
    emp.Height = $('#EmpPersonal_Height').val();
    emp.Weight = $('#EmpPersonal_Weight').val();
    emp.BloodType = $('#EmpPersonal_BloodType').val();
    emp.HomeAddress = $('#EmpPersonal_HomeAddress').val();
    emp.ZipCode = $('#EmpPersonal_ZipCode').val();
    emp.Telephone = $('#EmpPersonal_Telephone').val();
    emp.CurrentAddress = $('#EmpPersonal_CurrentAddress').val();
    emp.CurrentZipCode = $('#EmpPersonal_CurrentZipCode').val();
    emp.CurrentTelephone = $('#EmpPersonal_CurrentTelephone').val();
    emp.EmailAddress = $('#EmpPersonal_EmailAddress').val();
    emp.Cellphone = $('#EmpPersonal_Cellphone').val();
    emp.HDMFNo = $('#EmpPersonal_HDMFNo').val();
    emp.EYEEId = $('#EmpPersonal_EYEEId').val();
    emp.PHICNo = $('#EmpPersonal_PHICNo').val();
    emp.SSSNo = $('#EmpPersonal_SSSNo').val();
    emp.TIN = $('#EmpPersonal_TIN').val();
    emp.intMstTaxCode = $('#EmpPersonal_intMstTaxCode').val();
    emp.BankAccountNo = $('#EmpPersonal_BankAccountNo').val();

    emp.intMstEmpDTR = $('#DTR_intMstEmpDTR').val();
    emp.codeMstBranch = $('#DTR_codeMstBranch').val();
    emp.intMstDepartment = $('#DTR_intMstDepartment').val();
    emp.intMstPosition = $('#DTR_intMstPosition').val();
    emp.intMstWorkShift = $('#DTR_intMstWorkShift').val();
    emp.codeMstBranchHome = $('#DTR_codeMstBranchHome').val();
    emp.DateHired = $('#DTR_DateHired').val();
    emp.DateSeparated = $('#DTR_DateSeparated').val();
    emp.DateProbationary = $('#DTR_DateProbationary').val();
    emp.intMstWorkShift = $('#DTR_intMstWorkShift').val();
    emp.IsSupervisor = $('#DTR_IsSupervisor').val();
    emp.IsSupervisoryRate = $('#DTR_IsSupervisoryRate').val();
    emp.intMstEmpPersonalSupervisor = $('#DTR_intMstEmpPersonalSupervisor').val();
    emp.MonthlyRate = $('#DTR_MonthlyRate').val();
    emp.DailyRate = $('#DTR_DailyRate').val();
    emp.DiscAllowance = $('#DTR_DiscAllowance').val();
    emp.Cola = $('#DTR_Cola').val();
    emp.LateDeduction = $('#DTR_LateDeduction').val();
    emp.DTRPunches = $('#DTR_DTRPunches').val();
    emp.Biometrics = $('#DTR_Biometrics').val();
    emp.HDMFEmployee = $('#DTR_HDMFEmployee').val();
    emp.HDMFEmployer = $('#DTR_HDMFEmployer').val();
    emp.CoopMember = $('#DTR_CoopMember').val();
    emp.IncludeSSS = $('#DTR_IncludeSSS').val();
    emp.IncludePHIC = $('#DTR_IncludePHIC').val();
    emp.IncludeHDMF = $('#DTR_IncludeHDMF').val();
    emp.IncludeWithTax = $('#DTR_IncludeWithTax').val();
    emp.IncludeHealthInsurance = $('#DTR_IncludeHealthInsurance').val();
    emp.CoopDeposit = $('#DTR_CoopDeposit').val();
    emp.CoopSavings = $('#DTR_CoopSavings').val();
    emp.ReasonForSeparation = $('#DTR_ReasonForSeparation').val();
    emp.intBioRegistration = $('#DTR_intBioRegistration').val();    
    
    emp.intMstEmpFamily = $('#Family_intMstEmpFamily').val();
    emp.SpouseFirstname = $('#Family_SpouseFirstname').val();
    emp.SpouseLastname = $('#Family_SpouseLastname').val();
    emp.SpouseMiddlename = $('#Family_SpouseMiddlename').val();
    emp.SpouseTelephone = $('#Family_SpouseTelephone').val();
    emp.SpouseAddress = $('#Family_SpouseAddress').val();
    emp.FatherFirstname = $('#Family_FatherFirstname').val();
    emp.FatherLastname = $('#Family_FatherLastname').val();
    emp.FatherMiddlename = $('#Family_FatherMiddlename').val();
    emp.MotherLastname = $('#Family_MotherLastname').val();
    emp.MotherFirstname = $('#Family_MotherFirstname').val();
    emp.MotherMiddlename = $('#Family_MotherMiddlename').val();    


    //saving part
    $.ajax({
        url: url,
        data: JSON.stringify(emp),
        type: 'POST',
        contentType: 'application/json;',
        dataType: 'json',
        success: function (result) {

            $('#msg').addClass('active');
            $('#msg').removeClass('hide');

            if (result.success == "True") {
                $('#msg').removeClass('bg-danger');
                $('#msg').addClass('bg-success');
                setTimeout('HideMsg()', 2000);
            }
            else {
                $('#msg').removeClass('bg-success');
                $('#msg').addClass('bg-danger');
            }

            $('#msg').html(result.msg);
        }
    });
}