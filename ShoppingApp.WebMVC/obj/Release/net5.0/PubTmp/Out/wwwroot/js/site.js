function convertFirtLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1)
}

function convertToShortDate(dateString) {
    return new Date(dateString).toLocaleDateString('en-US');
}