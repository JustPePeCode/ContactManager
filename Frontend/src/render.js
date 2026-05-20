export function renderContacts(contacts,contactGrid){
    contactGrid.innerHTML = ""
    contacts.forEach(contact => {
  const card = document.createElement("div")
card.classList.add("card")
card.innerHTML = `
  <p class="name">${contact.name}</p>
  <p class="email">${contact.email}</p>
  <p class="gsm">${contact.gsm}</p>
  <button data-id ="${contact.id}">Change</button>
  <button data-id ="${contact.id}">Remove</button>
  
`
contactGrid.appendChild(card)
})
}