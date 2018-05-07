function ToggleReadOnly(idParam) {
    if (document.getElementById(idParam).readOnly == false) {
        document.getElementById(idParam).readOnly = true;
        document.getElementById(idParam + "icon").classList.remove('fa-lock-open');
        document.getElementById(idParam + "icon").classList.add('fa-lock');
    }
    else {
        document.getElementById(idParam).readOnly = false;
        document.getElementById(idParam + "icon").classList.remove('fa-lock');
        document.getElementById(idParam + "icon").classList.add('fa-lock-open');
    }
}