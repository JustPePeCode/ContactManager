import { selectedContactId } from "./state.js";
import { showElement, hideElement, getById } from "./ui-utils.js";
import { renderContacts } from "./render.js";
import { loadContacts, saveContacts } from "./storage.js";

const changeContact = getById("change-contact");
const changeContactInput = getById("change-contact-input");
const changeEmailInput = getById("change-email-input");
const changeGsmInput = getById("change-gsm-input");
const changeSubmitButton = getById("change-submit-button");
const changeCancelButton = getById("change-cancel-button");
const changeNameCantBeEmpty = getById("change-name-error");
const contactGrid = getById("contact-grid");
const contactList = getById("contact-list");

export function initChangeContact() {
  const component = {
    show: () => showElement(changeContact),
    onContactChanged: () => {},
    onChangeCanceled: () => {},
  };

  changeSubmitButton.addEventListener("click", () => {
    const contacts = loadContacts();
    const name = changeContactInput.value;
    const email = changeEmailInput.value;
    const gsm = changeGsmInput.value;
    if (name === "") {
      showElement(changeNameCantBeEmpty);
      return;
    }
    hideElement(changeNameCantBeEmpty);
    const updatedContacts = contacts.map((contact) => {
      if (contact.id === selectedContactId) {
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
    changeContactInput.value = "";
    changeEmailInput.value = "";
    changeGsmInput.value = "";
    saveContacts(updatedContacts);
    renderContacts(updatedContacts, contactGrid);
    hideElement(changeContact);
    showElement(contactList);
  });

  changeCancelButton.addEventListener("click", () => {
    hideElement(changeContact);
    hideElement(changeNameCantBeEmpty);
    showElement(contactList);
    changeContactInput.value = "";
    changeEmailInput.value = "";
    changeGsmInput.value = "";
  });
  return component;
}
