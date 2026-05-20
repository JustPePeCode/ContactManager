import { loadContacts, saveContacts } from "./storage.js";
import {
  normalizeSearchText,
  showElement,
  hideElement,
  getById,
} from "./ui-utils.js";
import { renderContacts } from "./render.js";

const contactGrid = getById("contact-grid");
const contactList = getById("contact-list");

const searchContactInput = getById("search-contact-input");
const searchContactButton = getById("search-contact-button");
const addContactButton = getById("add-contact-button");

const addContact = getById("add-contact");
const addContactInput = getById("add-contact-input");
const addEmailInput = getById("add-email-input");
const addGsmInput = getById("add-gsm-input");
const addSubmitButton = getById("add-submit-button");

const changeContact = getById("change-contact");
const changeContactInput = getById("change-contact-input");
const changeEmailInput = getById("change-email-input");
const changeGsmInput = getById("change-gsm-input");
const changeSubmitButton = getById("change-submit-button");

const removeContact = getById("remove-contact");
const removeContactCard = getById("remove-contact-card");
const RemoveConfirmButton = getById("confrim-remove-button")
const RemoveCancelButton = getById("cancel-remove-button")
let selectedContactId;

hideElement(addContact);
hideElement(changeContact);
hideElement(removeContact);

renderContacts(loadContacts(), contactGrid);

addSubmitButton.addEventListener("click", () => {
  const name = addContactInput.value;
  const email = addEmailInput.value;
  const gsm = addGsmInput.value;
   if (name ==="") {
  alert("name cannot be empty!");
  return ;
}
  const newContact = {
    id: crypto.randomUUID(),
    name: name,
    email: email,
    gsm: gsm,
  };
  addContactInput.value = "";
  addEmailInput.value = "";
  addGsmInput.value = "";
  const contacts = loadContacts();
  contacts.push(newContact);
  saveContacts(contacts);
  renderContacts(contacts, contactGrid);
  hideElement(addContact);
  showElement(contactGrid);
});

changeSubmitButton.addEventListener("click", () => {
  const contacts = loadContacts();
  const name = changeContactInput.value;
      const email = changeEmailInput.value;
      const gsm = changeGsmInput.value;
   if (name ==="") {
  alert("name cannot be empty!");
  return ;
}
  const updatedContacts = contacts.map((contact) => {
    if (contact.id === selectedContactId) {
      
     
      changeContactInput.value = "";
      changeEmailInput.value = "";
      changeGsmInput.value = "";
      const changedContact = {
        id: contact.id,
        name: name,
        email: email,
        gsm: gsm,
      };

      return changedContact;
    } else {
      return contact;
    }
  });

  saveContacts(updatedContacts);
  renderContacts(updatedContacts, contactGrid);
  hideElement(changeContact);
  showElement(contactGrid);
});

searchContactButton.addEventListener("click", () => {
  const searchValue = searchContactInput.value;
  const normalizedSearchValue = normalizeSearchText(searchValue);
  const contacts = loadContacts();
  const filteredContacts = contacts.filter((contact) => {
    return contact.name.toLowerCase().includes(normalizedSearchValue);
  });
  renderContacts(filteredContacts, contactGrid);
});

contactGrid.addEventListener("click", (event) => {
  const buttonName = event.target.textContent;
  const id = event.target.dataset.id;

  console.log(id);
  const contacts = loadContacts();
  if (buttonName == "Change") {
    selectedContactId = id;
    const selectedContact = contacts.find((contact) => contact.id === id);
    showElement(changeContact);
    hideElement(contactGrid)

    changeContactInput.value= selectedContact.name;
    changeEmailInput.value = selectedContact.email;
    changeGsmInput.value = selectedContact.gsm;
  }
  if (buttonName == "Remove") {
    selectedContactId=id
    hideElement(contactGrid)
    showElement(removeContact) 
    const selectedContact = contacts.find(contact => contact.id === id)
removeContactCard.innerHTML = `
  <p class="name">${selectedContact.name}</p>
  <p class="email">${selectedContact.email}</p>
  <p class="gsm">${selectedContact.gsm}</p>`
  }
});

RemoveConfirmButton.addEventListener("click", () => {
  const contacs = loadContacts()
  const showContacts = contacs.filter(contact=> {
    return contact.id !=selectedContactId
  })
  
   saveContacts(showContacts);
  renderContacts(showContacts, contactGrid);
  hideElement(removeContact)
  showElement(contactGrid)
})
RemoveCancelButton.addEventListener("click", () => {
  hideElement(removeContact)
  showElement(contactGrid)
})

addContactButton.addEventListener("click", () => {
  showElement(addContact);
  hideElement(contactGrid)
});
