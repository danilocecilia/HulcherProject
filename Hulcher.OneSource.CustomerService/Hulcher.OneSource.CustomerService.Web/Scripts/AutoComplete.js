var timeoutId;

function setFocusTimeout(controlID) {
    focusControlID = controlID;
    timeoutId = setTimeout("setFocusAutoComplete('" + focusControlID + "')", 100);
}

function setFocusAutoComplete(focusControlID) {
    document.getElementById(focusControlID).focus();
    clearTimeout(timeoutId);
}