import { useContacts } from "../queries/contactQueries";
import { ContactCard } from "./ContactCard";



export function ContactList() {
  const { data, isLoading } = useContacts();

  if (isLoading) {
    return <p>laden . . . </p>
  }
  
  if(data?.length===0)
  {
    return <p>no contacts</p>;
  }

  return (
    <section>
      {data?.map(contact => (
        <ContactCard key={contact.id}id = {contact.id} name={contact.name} email = { contact.email } phone={contact.phone} />
      ))}
    </section>
  );
}