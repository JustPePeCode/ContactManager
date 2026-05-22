import { selectedContactId } from "./state.js";
import { showElement, hideElement, getById } from "./ui-utils.js";
import { loadContacts, saveContacts } from "./storage.js";

const changeContact = getById("change-contact");
const changeContactInput = getById("change-contact-input");
const changeEmailInput = getById("change-email-input");
const changeGsmInput = getById("change-gsm-input");
const changeSubmitButton = getById("change-submit-button");
const changeCancelButton = getById("change-cancel-button");
const changeNameCantBeEmpty = getById("change-name-error");


export function initChangeContact() {
  const component = {
    show: () => {
        const contact = loadContacts().find((c) => c.id === selectedContactId);
 changeContactInput.value=contact.name;
 changeEmailInput.value=contact.email;
 changeGsmInput.value= contact.gsm;
 showElement(changeContact)
    },
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
    hideElement(changeContact)
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
    component.onContactChanged()
  });

  changeCancelButton.addEventListener("click", () => {
    hideElement(changeNameCantBeEmpty);
    hideElement(changeContact)
    changeContactInput.value = "";
    changeEmailInput.value = "";
    changeGsmInput.value = "";
    component.onChangeCanceled();
  });
  return component;
}
