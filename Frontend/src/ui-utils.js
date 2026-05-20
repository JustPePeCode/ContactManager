export function normalizeSearchText(value){

return value.trim().toLowerCase()

}
export function showElement(element){
    element.classList.remove("hidden")

}
export function hideElement(element){
    element.classList.add("hidden")

}

export function getById(id) {
    const element = document.getElementById(id);
    if (element === null)
        throw new Error(`Could not find element with id '${id}'.`);
    return element;
}