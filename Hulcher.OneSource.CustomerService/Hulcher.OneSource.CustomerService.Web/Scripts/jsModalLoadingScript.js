//  register for our events
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

function beginRequest(sender, args) {
    // show the popup
    var elem = args.get_postBackElement();
    if (elem != undefined && elem.id.indexOf('btnFakeSort') == -1)
        $find('mdlPopUpExtender').show();
}

function endRequest(sender, args) {
    $find('mdlPopUpExtender').hide();
}