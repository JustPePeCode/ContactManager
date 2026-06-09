import type {
  Contact,
  CreateContactRequest,
  UpdateContactRequest,
  CreateContactResponse,
} from "../types/contacts";

export async function getContacts(): Promise<Contact[]> {
  const response = await fetch("/api/contacts");

  if (!response.ok) {
    throw new Error("Kon contacten niet ophalen");
  }

  return await response.json();
}

export async function searchContacts(name:string): Promise<Contact[]> {
  const response = await fetch(`/api/contacts/search?name=${name}`);

  if (!response.ok) {
    throw new Error("Kon geen contacten vinden");
  }

  return await response.json();
}

export async function createContact(request: CreateContactRequest): Promise<CreateContactResponse> {
  const response = await fetch("/api/contacts", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error("Kon contact niet toevoegen");
  }

  return await response.json();
}

export async function updateContact(id :number, request: UpdateContactRequest): Promise<Contact> {
  const response = await fetch(`/api/contacts/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error("Kon contact niet veranderen");
  }

  return await response.json();
}

export async function deleteContact(id:number): Promise<void> {
  const response = await fetch(`/api/contacts/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Kon contact niet verwijderen");
  }
}
