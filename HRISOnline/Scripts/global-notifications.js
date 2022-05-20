function GetNotifications(urlString, sessUsername, urlEmpNotification)
{
    $.get(
        urlString,
        function (data) {
            
            $('#for-approval ul').empty();

            var x;            
            if ((sessUsername != null) || (sessUsername != ''))
            {
                var arr = JSON.parse(data.obj);

                //announcements
                if (parseInt(arr["TotalCount"]) == 0) {
                    $('#bg-announcements').html('');
                }
                else {
                    $('#bg-announcements').html("!");
                }

                //memo
                if (parseInt(arr["MemoCount"]) == 0) {
                    $('#bg-memo').html('');
                }
                else {
                    $('#bg-memo').html(arr["MemoCount"]);
                }

                //notification
                if (parseInt(arr["NotificationCount"]) == 0) {
                    $('#bg-notification').html('');
                }
                else {
                    $('#bg-notification').html(arr["NotificationCount"]);
                }

                //emp notice
                if (parseInt(arr["EmpNoticeCount"]) == 0) {
                    $('#bg-empnotice').html('');
                }
                else {
                    $('#bg-empnotice').html(arr["EmpNoticeCount"]);
                }

                //emp Audit Report
                if (parseInt(arr["EmpAuditCount"]) == 0) {
                    $('#bg-empaudit').html('');
                }
                else {
                    $('#bg-empaudit').html(arr["EmpAuditCount"]);
                }

                //WITHOUT DEVIATION
                if (parseInt(arr["WithOutDeviation"]) == 0) {
                    $('#bg-empauditWithout').html('');
                }
                else {
                    $('#bg-empauditWithout').html(arr["WithOutDeviation"]);
                }
                //WITH MINOR DEVIATION 
                if (parseInt(arr["MinorDeviation"]) == 0) {
                    $('#bg-empauditMinor').html('');
                }
                else {
                    $('#bg-empauditMinor').html(arr["MinorDeviation"]);
                }
                //WITH MAJOR DEVIATION 
                if (parseInt(arr["MajorDeviation"]) == 0) {
                    $('#bg-empauditMajor').html('');
                }
                else {
                    $('#bg-empauditMajor').html(arr["MajorDeviation"]);
                }
                //NOT TAG DEVIATION PREVIOUS AUDIT REPORT
                if (parseInt(arr["PrevAuditReport"]) == 0) {
                    $('#bg-empauditPrev').html('');
                }
                else {
                    $('#bg-empauditPrev').html(arr["PrevAuditReport"]);
                }
                

                //policy
                if (parseInt(arr["PolicyCount"]) == 0) {
                    $('#bg-policy').html('');
                }
                else {
                    $('#bg-policy').html(arr["PolicyCount"]);
                }

                //employee approval notification
                if (parseInt(arr["EmpApprovalNotification"]) != 0) {                                      
                    
                    $("#for-approval ul").append('<li><a href=' + urlEmpNotification + '><div>Approved/Disapproved</div></a></li>');

                    $('#for-approval').removeClass('hide');
                    $('#for-approval').show();
                }
                

                //===============================================================================//
                //one profile
                if (parseInt(arr["TotalPendingCount"]) == 0) {
                    $('#bg-oneprofile-p').html('');
                }
                else {
                    $('#bg-oneprofile-p').html("p");
                }

                //ot
                if (parseInt(arr["OTPendingCount"]) == 0) {
                    $('#bg-ot-p').html('');
                    $('#bg-ot-p1').html('');
                }
                else {
                    $('#bg-ot-p').html("p");
                    $('#bg-ot-p1').html("p");
                }

                //leave
                if (parseInt(arr["LeavePendingCount"]) == 0) {
                    $('#bg-lv-p').html('');
                    $('#bg-lv-p1').html('');
                }
                else {
                    $('#bg-lv-p').html("p");
                    $('#bg-lv-p1').html("p");
                }               

                //ut
                if (parseInt(arr["UTPendingCount"]) == 0) {
                    $('#bg-pb-p').html('');
                    $('#bg-pb-p1').html('');
                }
                else {
                    $('#bg-pb-p').html("p");
                    $('#bg-pb-p1').html("p");
                }

                //ob
                if (parseInt(arr["OBPendingCount"]) == 0) {
                    $('#bg-ob-p').html('');
                    $('#bg-ob-p1').html('');
                }
                else {
                    $('#bg-ob-p').html("p");
                    $('#bg-ob-p1').html("p");
                }

                //missing punch
                if (parseInt(arr["MPPendingCount"]) == 0) {
                    $('#bg-mp-p').html('');
                    $('#bg-mp-p1').html('');
                }
                else {
                    $('#bg-mp-p').html("p");
                    $('#bg-mp-p1').html("p");
                }

                //OvertimeMeals
                if (parseInt(arr["OTMealsCount"]) == 0) {
                    $('#bg-ot-p').html('');
                    $('#bg-otm-p2').html('');
                }
                else {
                    $('#bg-ot-p').html("p");
                    $('#bg-otm-p2').html("p");
                }

            }
            
        });
}


//function GetItemsForApproval(urlString) {    

//    $.get(
//        urlString,
//        function (data) {

//            if (data.otCount != "0") {
//                $('#bg-ot').html(data.otCount);
//                $('#bg-ot-2').html(data.otCount);
//            }
//            if (data.fmCount != "0") {
//                $('#bg-ot').html(data.fmCount);
//                $('#bg-ot-2').html(data.fmCount);
//            }
//            if (data.leaveCount != "0") {
//                $('#bg-lv').html(data.leaveCount);
//                $('#bg-lv-1').html(data.leaveCount);
//            }
//            if (data.obCount != "0") {
//                $('#bg-ob').html(data.obCount);
//                $('#bg-ob-1').html(data.obCount);
//            }
//            if (data.pbCount != "0") {
//                $('#bg-pb').html(data.pbCount);
//                $('#bg-pb-1').html(data.pbCount);
//            }

//            if ((data.otCount != "0") || (data.fmCount != "0") || (data.leaveCount != "0") || (data.obCount != "0") || (data.pbCount != "0")) {
//                $('#bg-oneprofile').html("!");
//            }

//        }
//    );
//}