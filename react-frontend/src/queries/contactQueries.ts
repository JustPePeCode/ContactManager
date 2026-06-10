import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getContacts, createContact, searchContacts, updateContact,deleteContact } from "../api/contactsApi";
import type { UpdateContactRequest } from "../types/contacts";

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
export function useUpdateContact(){
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn:({id,request}:{id: number, request:UpdateContactRequest}) => updateContact(id,request),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["contacts"]})
        }
    })
}
export function useDeleteContact(){
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn:(id:number) => deleteContact(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["contacts"]})
        }
    })
}