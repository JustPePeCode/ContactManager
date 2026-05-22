import { selectedContactId, setSelectedContactId } from "./state.js";

const removeContact = getById("remove-contact");
const removeContactCard = getById("remove-contact-card");
const removeConfirmButton = getById("remove-confirm-button");
const removeCancelButton = getById("remove-cancel-button");

export function initRemoveContact(){
removeConfirmButton.addEventListener("click", () => {
  const contacts = loadContacts();
  const showContacts = contacts.filter((contact) => {
    return contact.id != selectedContactId;
  });

  saveContacts(showContacts);
  renderContacts(showContacts, contactGrid);
  hideElement(removeContact);
  showElement(contactList);
});
removeCancelButton.addEventListener("click", () => {
  hideElement(removeContact);
  showElement(contactList);
});
}
