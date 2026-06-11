import type { Contact } from "../types/contacts";


export function ContactCard({ name, email, phone }: Contact) {
  return (
    <article>
      <h2>{name}</h2>
      <p>{email}</p>
      <p>{phone}</p>
    </article>
  );
}
