<!--

    // Function to maintain original default states for all fields.
    //  In order to test for dirtiness, we will be checking if the default
    //  value for each control matches its current value. However, this
    //  default is not normally preserved across partial postbacks. We need to
    //  preserve these values ourselves.
    function keepDefaults(form) {

        // Get a reference to the form (in ASP.Net there should only be the one).
        var form = document.forms[0];

        // If no original values are yet preserved...
        if (typeof (document.originalValues) == "undefined") {

            // Create somewhere to store the values.
            document.originalValues = new Array();

        }

        // For each of the fields on the page...
        for (var i = 0; i < form.elements.length; i++) {

            // Get a ref to the field.
            var field = form.elements[i];

            // Depending on the type of the field...
            switch (field.type) {

                // For simple value elements...     
                case "text":
                case "file":
                case "password":
                case "textarea":

                    // If we don't yet know the original value...
                    if (typeof (document.originalValues[field.id]) == "undefined") {

                        // Save it for later.
                        document.originalValues[field.id] = field.value;

                    }
                    break;

                // For checkable elements...     
                case "checkbox":
                case "radio":

                    // If we don't yet know the original check state...
                    if (typeof (document.originalValues[field.id]) == "undefined") {

                        // Save it for later.
                        document.originalValues[field.id] = field.checked;

                    }
                    break;

                // For selection elements...     
                case "select-multiple":
                case "select-one":

                    // The form is dirty if the selection has changed.

                    // For each of the options...
                    var options = field.options;
                    for (var j = 0; j < options.length; j++) {

                        var optId = field.id + "_" + j;

                        // If we don't yet know the original selection state...
                        if (typeof (document.originalValues[optId]) == "undefined") {

                            // Save it for later.
                            document.originalValues[optId] = options[j].selected;

                        }
                    }
                    break;
            }

        }
    }

    // Call function to preserve defaults every time the page is loaded (or is
    // posted back).
    Sys.Application.add_load(keepDefaults);

    // Assume that a check for dirtiness is required.
    //  If this value is still true, we will check for dirtiness when the page
    //  unloads.
    var dirtyCheckNeeded = true;

    // Function to flag that a check for dirtiness is not required.
    //  Called by Save and Cancel buttons to indicate that a dirty check is
    //  not actually required.
    function ignoreDirty() {
        dirtyCheckNeeded = false;
    }

    // Function to check if the page is dirty.
    //  The function compares the default value for the control (the one it
    //  was given when the page loaded) with its current value.
    function isDirty(form) {

        if (typeof (document.originalValues) != "undefined") {
            // For each of the fields on the page...
            for (var i = 0; i < form.elements.length; i++) {
                var field = form.elements[i];

                // Depending on the type of the field...
                switch (field.type) {

                    // For simple value elements...     
                    case "text":
                    case "file":
                    case "password":
                    case "textarea":

                        // The form is dirty if the value has changed.
                        if (field.value != document.originalValues[field.id]) {
                            // Uncomment the next line for debugging.
                            //alert(field.type + ' ' + field.id + ' ' + field.value + ' ' + document.originalValues[field.id]);
                            return true;
                        }
                        break;

                    // For checkable elements...     
                    case "checkbox":
                    case "radio":

                        // The form is dirty if the check has changed.
                        if (field.checked != document.originalValues[field.id]) {
                            // Uncomment the next line for debugging.
                            //alert(field.type + ' ' + field.id + ' ' + field.checked + ' ' + document.originalValues[field.id]);
                            return true;
                        }
                        break;

                    // For selection elements...     
                    case "select-multiple":
                    case "select-one":

                        // The form is dirty if the selection has changed.
                        var options = field.options;
                        for (var j = 0; j < options.length; j++) {
                            var optId = field.id + "_" + j;
                            if (options[j].selected != document.originalValues[optId]) {

                                // Uncomment the next line for debugging.
                                //alert(field.type + ' ' + field.id + ' ' + options[j].text + ' ' + options[j].selected + ' ' + document.originalValues[optId]);
                                return true;
                            }
                        }
                        break;
                }
            }

            // The form is not dirty.
            return false;
        }

        return true;
    }

    // Clicking on some controls in (at least) IE6 caused the onbeforeunload
    // to fire *twice*. We use this flag to check for this condition.
    var onBeforeUnloadFired = false;

    // Function to reset the above flag.
    function resetOnBeforeUnloadFired() {
        onBeforeUnloadFired = false;
    }

    // Handle the beforeunload event of the page.
    //  This will be called when the user navigates away from the page using
    //  controls on the page or browser navigation (back, refresh, history,
    //  close etc.). It is not called for partial post-backs.
    function doBeforeUnload() {

        // If this function has not been run before...
        if (!onBeforeUnloadFired) 
        {

            // Prevent this function from being run twice in succession.
            onBeforeUnloadFired = true;

            // If the dirty check is required...
            if (dirtyCheckNeeded) {

                // If the form is dirty...
                if (isDirty(document.forms[0])) {

                    // Ask the user if she is sure she wants to continue.
                    event.returnValue = "Are you sure you want to cancel? Your information will not be saved.";

                }
            }
            else {
                dirtyCheckNeeded = true;
            }
        }


        // If the user clicks cancel, allow the onbeforeunload function to run again.
        window.setTimeout("resetOnBeforeUnloadFired()", 1000);
    }



    function doBeforeSave(hfSave, ckbCopyGeneralLog) 
    {
   
            // If the dirty check is required...
            if (dirtyCheckNeeded) 
            {
              
                // If the form is dirty...
                if (isDirty(document.forms[0]) && document.getElementById(hfSave).value == "") 
                {
                  
                   document.getElementById(hfSave).value  = "0";
                   if (document.getElementById(ckbCopyGeneralLog)) {
                       document.getElementById(ckbCopyGeneralLog).disabled = false;
                       document.getElementById(ckbCopyGeneralLog).checked = false;
                   }
                }
                else
                {
                    window.setTimeout("doBeforeSave('" + hfSave + "', '" + ckbCopyGeneralLog + "')", 1000);
                }
            }
    }

    // Hook the beforeunload event of the page.
    //  Call the dirty check when the page unloads.
//    if (window.body) {
//        // IE
//        window.body.onbeforeunload = doBeforeUnload;
//    }
//    else
//    // FX
//        window.onbeforeunload = doBeforeUnload;

// -->