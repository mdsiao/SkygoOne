function disableWeekends(dtpicker, hasSaturday) {
    var days = dtpicker.find('.datepicker-days tr').each(function () {
        var day = $(this).find('.day');
        day.eq(0).addClass('old').click(false); //Sunday     

        if (hasSaturday == "False") {
            day.eq(6).addClass('old').click(false); //Saturday
        }
    });

}