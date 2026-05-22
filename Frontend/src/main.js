import { loadContacts, saveContacts } from "./storage.js";
import {
  normalizeSearchText,
  showElement,
  hideElement,
  getById,
} from "./ui-utils.js";
import { renderContacts } from "./render.js";
import { initAddContact } from "./addContact.js";


const addContact = initAddContact();
const changeContact = initChangeContact();
const removeContact = initRemoveContact();
const contactGrid = getById("contact-grid");
const contactList = getById("contact-list");

const searchContactInput = getById("search-contact-input");
const searchContactButton = getById("search-contact-button");
const quitButton = getById("quit-contactmanger-button");
const addContactComponent = initAddContact();


addContactComponent.onContactCreated = () => {
    renderContacts(loadContacts(), contactGrid);
    showElement(contactList);
};
addContactComponent.onAddCanceled = () => {
    showElement(contactList);
};

renderContacts(loadContacts(), contactGrid);



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
    hideElement(contactList);

    changeContactInput.value = selectedContact.name;
    changeEmailInput.value = selectedContact.email;
    changeGsmInput.value = selectedContact.gsm;
  }
  if (buttonName == "Remove") {
    selectedContactId = id;
    hideElement(contactList);
    showElement(removeContact);
    const selectedContact = contacts.find((contact) => contact.id === id);
    removeContactCard.innerHTML = `
  <p class="name">Name: ${selectedContact.name}</p>
  <p class="email">Email:${selectedContact.email}</p>
  <p class="gsm">Gsm: ${selectedContact.gsm}</p>`;
  }
});

quitButton.addEventListener("click", () => {
  window.close();
});


