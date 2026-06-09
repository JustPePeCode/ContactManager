export type Contact = {
  id: number;
  name: string;
  email: string;
  phone: string;
};

export type CreateContactRequest = {
  name: string;
  email: string;
  phone: string;
};

export type CreateContactResponse = Contact;

export type UpdateContactRequest = {
  name: string;
  email: string;
  phone: string;
};

export type GetAllContactResponse = Contact;

export type SearchContactResponse = Contact;