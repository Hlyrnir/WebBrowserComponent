function showAlert(message) {
    alert(message);
}

function showConfirm(message) {
    return confirm(message);
};

function showDialog(dialog) {
    if (!dialog.open) {
        dialog.showModal();
    }
};

function closeDialog(dialog) {
    if (dialog.open) {
        dialog.close();
    }
};

function showPrompt(message, defaultValue) {
    return prompt(message, defaultValue);
}

function checkMediaQueryListMatch(query) {
    const mediaQueryList = window.matchMedia(query);

    return mediaQueryList.matches;
}

function addMediaQueryListChangedListener(query, methodName, dotNetObjectReference) {
    const mediaQueryList = window.matchMedia(query);

    mediaQueryList.addEventListener('change', (event) => {
        dotNetObjectReference.invokeMethodAsync(methodName, event.matches);
    });
}

function removeMediaQueryListChangedListener(query, methodName, dotNetObjectReference) {
    const mediaQueryList = window.matchMedia(query);

    mediaQueryList.removeEventListener('change', (event) => {
        dotNetObjectReference.invokeMethodAsync(methodName, event.matches);
    });
}