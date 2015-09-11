$(document).ready(setErrorBoxBehavior);

//  register for our events
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

function endRequest(sender, args) {
    setErrorBoxBehavior();
}

function setErrorBoxBehavior() {
    $('div.errorbox').click(
        function () {
            $(document).find('.ScrollableGridView').css('visibility', 'hidden');
            //$(this).slideUp('fast');
            $(this).css('display', 'none');
            $(document).find('.ScrollableGridView').css('visibility', 'visible');
        }
    );
}