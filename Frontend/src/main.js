import {loadContacts,saveContacts} from './storage.js'
import {normalizeSearchText,showElement,hideElement} from './ui-utils.js'



const contactGrid = document.getElementById("contact-grid")
const contactList= document.getElementById("contact-list")

const searchContactInput = document.getElementById("search-contact-input")
const searchContactButton = document.getElementById("search-contact-button")
const addContactButton = document.getElementById("add-contact-button")

const addContact = document.getElementById("add-contact")
const addContactInput = document.getElementById("add-contact-input")
const addEmailInput = document.getElementById("add-email-input")
const addGsmInput = document.getElementById("add-gsm-input")
const addSubmitButton = document.getElementById("add-submit-button")

const changeContact = document.getElementById("change-contact")
const changeContactInput = document.getElementById("change-contact-input")
const changeEmailInput = document.getElementById("change-email-input")
const changeGsmInput = document.getElementById("change-gsm-input")
const changeSubmitButton = document.getElementById("change-submit-button")

const removeContact = document.getElementById("remove-contact")
const removeContactCard = document.getElementById("remove-contact-card")

let selectedContactId

hideElement(addContact)
hideElement(changeContact)
hideElement(removeContact)

function renderContacts(contacts){
    contactGrid.innerHTML = ""
    contacts.forEach(contact => {
  const card = document.createElement("div")
card.classList.add("card")
card.innerHTML = `
  <p class="name">${contact.name}</p>
  <p class="email">${contact.email}</p>
  <p class="gsm">${contact.gsm}</p>
  <button data-id ="${contact.id}">Change</button>
  <button data-id ="${contact.id}">Remove</button>
  
`
contactGrid.appendChild(card)
})
}

renderContacts(loadContacts())

addSubmitButton.addEventListener("click", () => {
  
    const name = addContactInput.value
    const email = addEmailInput.value
    const gsm = addGsmInput.value
   const newContact= { id: crypto.randomUUID(), name: name, email: email, gsm: gsm }
 addContactInput.value = ""
 addEmailInput.value = ""
 addGsmInput.value = ""
 const contacts=loadContacts()
 contacts.push(newContact)
 saveContacts(contacts) 
 renderContacts(contacts)
 hideElement(addContact)

})

changeSubmitButton.addEventListener("click", () => {
  const contacts=loadContacts()
  const updatedContacts=contacts.map(contact => {
    if (contact.id === selectedContactId) {
      const name = changeContactInput.value
    const email = changeEmailInput.value
    const gsm = changeGsmInput.value
 changeContactInput.value = ""
 changeEmailInput.value = ""
 changeGsmInput.value = ""
 const changedContact= { id: contact.id, name: name, email: email, gsm: gsm }
 
 return changedContact
}

 else{
  return contact
 }
})
 
 saveContacts(updatedContacts) 
 renderContacts(updatedContacts)
     hideElement(changeContact)
  } 
)

searchContactButton.addEventListener("click", ()=>{
  
    const searchValue = searchContactInput.value
    const normalizedSearchValue =normalizeSearchText(searchValue)
    const contacts =loadContacts()
   const filteredContacts =  contacts.filter(contact=>{
        return contact.name.toLowerCase().includes(normalizedSearchValue)
    })
    renderContacts(filteredContacts)
})
contactGrid.addEventListener("click", (event) => {
  
   const buttonName =event.target.textContent
  const id = event.target.dataset.id
    console.log(id)
    const contacts = loadContacts()
    if (buttonName =="Change") {
      selectedContactId = id
      showElement(changeContact)
    }
    if (buttonName== "Remove") {
      const showContacts = contacts.filter(contact=>{
      return contact.id!=id
    })
       saveContacts(showContacts)
    renderContacts(showContacts)
    }
   
})

addContactButton.addEventListener("click", ()=>{
  showElement(addContact)
})
