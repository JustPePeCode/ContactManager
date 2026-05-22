import { loadContacts } from "./storage.js";
import {
  normalizeSearchText,
  showElement,
  hideElement,
  getById,
} from "./ui-utils.js";
import { renderContacts } from "./render.js";
import { initAddContact } from "./addContact.js";
import { initChangeContact } from "./changeContact.js";
import { initRemoveContact } from "./removeContact.js";
import { setSelectedContactId } from "./state.js";

const searchContactInput = getById("search-contact-input");
const searchContactButton = getById("search-contact-button");
const quitButton = getById("quit-contactmanger-button");
const addContactComponent = initAddContact();
const changeContactComponent = initChangeContact();
const removeContactComponent = initRemoveContact();
const contactGrid = getById("contact-grid");
const contactList = getById("contact-list");

addContactComponent.onContactCreated = () => {
  renderContacts(loadContacts(), contactGrid);
  showElement(contactList);
};
addContactComponent.onAddCanceled = () => {
  showElement(contactList);
};

changeContactComponent.onContactChanged = () => {
  renderContacts(loadContacts(), contactGrid);
  showElement(contactList);
};
changeContactComponent.onChangeCanceled = () => {
  showElement(contactList);
};

removeContactComponent.onContactRemoved = () => {
  renderContacts(loadContacts(),contactGrid)
  showElement(contactList)
}
removeContactComponent.onRemoveCanceled = () => {
  showElement(contactList)
}

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

  if (buttonName == "Change") {
    setSelectedContactId(id);
    changeContactComponent.show();
    hideElement(contactList);
  }
  if (buttonName == "Remove") {
    setSelectedContactId(id);
    removeContactComponent.show();
    hideElement(contactList);
  }
});

quitButton.addEventListener("click", () => {
  window.close();
});
