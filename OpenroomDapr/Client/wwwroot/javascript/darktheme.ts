export function onDisplayModeChanged(mode) {
    const lightElement = document.getElementById('default-theme');
    const darkElement = document.getElementById('sonar-dark-theme');
    console.info({ mode });
    if (mode === 'light') {
        localStorage.setItem('DisplayMode', 'light');
        enableStylesheet(lightElement);
        disableStylesheet(darkElement);
    }
    if (mode === 'dark' || (mode === 'system' && window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        localStorage.setItem('DisplayMode', 'dark');
        enableStylesheet(darkElement);
        disableStylesheet(lightElement);
    }
    else {
        localStorage.removeItem('DisplayMode');
    }
}

function enableStylesheet(node) {
    node.media = '';
    node.rel = 'stylesheet';
}

function disableStylesheet(node) {
    node.media = 'none';
    node.rel = 'alternate stylesheet'
}

window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', event => {
    const newColorScheme = event.matches ? "dark" : "light";
    console.info("change detected");
    console.info({ newColorScheme });
});