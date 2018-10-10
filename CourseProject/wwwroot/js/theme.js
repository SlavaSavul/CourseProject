
function setTheme() {
    let themeIndex = document.getElementById('themeForm').options.selectedIndex;
    let theme;
    if (themeIndex == 0) 
        theme = 'MathTheme';
    else
        theme = 'GumTheme';
    setCookie('ThemeName', theme, 365);
    changeCSS();
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function changeCSS() {
    var cookie = getCookie('ThemeName');
    var href = "/css/";
    if (cookie != null) {
        if (cookie === 'MathTheme') {
            href += "mathTheme.css";
        }
        else if (cookie === 'GumTheme') {
            href += "gumTheme.css";
        }
    }
    else {
        href += "mathTheme.css";
    }
    var link = document.getElementById('themeFile');
    link.setAttribute('href', href)
}

