export function normalizeSearchText(value){

return value.trim().toLowerCase()

}
export function showElement(element){
    element.classList.remove("hidden")

}
export function hideElement(element){
    element.classList.add("hidden")

}