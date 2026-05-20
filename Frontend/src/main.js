import {loadContacts,saveContacts} from './storage.js'
import {normalizeSearchText,showElement,hideElement} from './ui-utils.js'



const contactGrid = GetById("contact-grid")
const contactList= GetById("contact-list")

const searchContactInput = GetById("search-contact-input")
const searchContactButton = GetById("search-contact-button")
const addContactButton = GetById("add-contact-button")

const addContact = GetById("add-contact")
const addContactInput = GetById("add-contact-input")
const addEmailInput = GetById("add-email-input")
const addGsmInput = GetById("add-gsm-input")
const addSubmitButton = GetById("add-submit-button")

const changeContact = GetById("change-contact")
const changeContactInput = GetById("change-contact-input")
const changeEmailInput = GetById("change-email-input")
const changeGsmInput = GetById("change-gsm-input")
const changeSubmitButton = GetById("change-submit-button")

const removeContact = GetById("remove-contact")
const removeContactCard = GetById("remove-contact-card")

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
