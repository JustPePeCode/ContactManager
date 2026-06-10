import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getContacts, createContact, searchContacts } from "../api/contactsApi";

export function useContacts() {
  return useQuery({
    queryKey: ["contacts"],
    queryFn: getContacts
  });
}

export function useCreateContact() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createContact,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["contacts"] });
    }
  });
}
export function useSearchContacts(name:string){
    return useQuery({
        queryKey: ["contacts",name],
        queryFn: () =>searchContacts(name)
    });
}