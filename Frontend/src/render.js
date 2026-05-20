export function renderContacts(contacts, contactGrid) {
  contactGrid.innerHTML = "";
  contacts.forEach((contact) => {
    const card = document.createElement("div");
    card.classList.add("card");
    card.innerHTML = `
  <p class="name">Name: ${contact.name}</p>
  <p class="email">Email: ${contact.email}</p>
  <p class="gsm">Gsm: ${contact.gsm}</p>
  <button data-id ="${contact.id}">Change</button>
  <button data-id ="${contact.id}">Remove</button>
  
`;
    contactGrid.appendChild(card);
  });
}
