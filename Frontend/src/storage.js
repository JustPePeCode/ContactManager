const storageKey = "contacts";
export function loadContacts(){
const json=localStorage.getItem(storageKey)
if (json===null) {
   return [] 
}
return JSON.parse(json)
}


export function saveContacts(contacts){
    localStorage.setItem(storageKey,JSON.stringify(contacts))
    
}