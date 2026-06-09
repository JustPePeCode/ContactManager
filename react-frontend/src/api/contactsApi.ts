getContacts()
searchContacts(name)
createContact(request)
updateContact(id, request)
deleteContact(id)

import type { Contact } from "../types/contacts";

export async function getContacts(): Promise<Contact[]> {
  const response = await fetch("/api/contacts");

  if (!response.ok) {
    throw new Error("Kon contacten niet ophalen");
  }

  return await response.json();
}


export async function searchContacts(name): Promise<Contact[]> {
  const response = await fetch(`/api/contacts/search?name=${name}`);

  if (!response.ok) {
    throw new Error("Kon geen contacten vinden");
  }

  return await response.json();
}