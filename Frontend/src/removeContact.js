import { selectedContactId } from "./state.js";
import { getById, hideElement, showElement } from "./ui-utils.js";
import { loadContacts, saveContacts } from "./storage.js";

const removeContact = getById("remove-contact");
const removeContactCard = getById("remove-contact-card");
const removeConfirmButton = getById("remove-confirm-button");
const removeCancelButton = getById("remove-cancel-button");

export function initRemoveContact() {
  const component = {
    show: () => {
      removeContactCard.innerHTML = "";
      const contact = loadContacts().find((c) => c.id === selectedContactId);
      const card = document.createElement("div");
      card.classList.add("card");

      const nameP = document.createElement("p");
      nameP.classList.add("name");
      nameP.textContent = contact.name;
      card.appendChild(nameP);

      const emailP = document.createElement("p");
      emailP.classList.add("email");
      emailP.textContent = contact.email;
      card.appendChild(emailP);

      const gsmP = document.createElement("p");
      gsmP.classList.add("gsm");
      gsmP.textContent = contact.gsm;
      card.appendChild(gsmP);

      removeContactCard.appendChild(card);
      showElement(removeContact);
    },
    onContactRemoved: () => {},
    onRemoveCanceled: () => {},
  };

  removeConfirmButton.addEventListener("click", () => {
    const contacts = loadContacts();
    const showContacts = contacts.filter((contact) => {
      return contact.id != selectedContactId;
    });

    saveContacts(showContacts);
    hideElement(removeContact);
    component.onContactRemoved();
  });
  removeCancelButton.addEventListener("click", () => {
    hideElement(removeContact);
    component.onRemoveCanceled();
  });
  return component;
}
