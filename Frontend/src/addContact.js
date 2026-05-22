import { showElement, hideElement, getById } from "./ui-utils.js";

import { loadContacts, saveContacts } from "./storage.js";

const addContactButton = getById("add-contact-button");
const addContactPanel = getById("add-contact");
const addContactInput = getById("add-contact-input");
const addEmailInput = getById("add-email-input");
const addGsmInput = getById("add-gsm-input");
const addSubmitButton = getById("add-submit-button");
const addCancelButton = getById("add-cancel-button");
const addNameCantBeEmpty = getById("add-name-error");

export function initAddContact() {
  const component = {
    show: () => showElement(addContactPanel),
    onContactCreated: () => {},
    onAddCanceled: () => {},
  };
  addContactButton.addEventListener("click", () => component.show());
  addSubmitButton.addEventListener("click", () => {
    const name = addContactInput.value;
    const email = addEmailInput.value;
    const gsm = addGsmInput.value;
    if (name === "") {
      showElement(addNameCantBeEmpty);
      return;
    }
    hideElement(addNameCantBeEmpty);
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
    hideElement(addContactPanel);
    component.onContactCreated();
  });

  addCancelButton.addEventListener("click", () =>  {
    hideElement(addContactPanel);
    hideElement(addNameCantBeEmpty);
    addContactInput.value = "";
    addEmailInput.value = "";
    addGsmInput.value = "";
    component.onAddCanceled();
  });
  return component;
}
