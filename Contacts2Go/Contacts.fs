namespace Contacts2Go

open Android.App
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open Android.Graphics
open Android.Provider
open Android.Content

module Contacts =

    let getContactsInfo (cur:Android.Database.ICursor) : (string * string)[] =
        [ while cur.MoveToNext() do yield (cur.GetString(0), cur.GetString(1)) ]
        |> Array.ofList

    let GetContactCursor (activity:Activity) =
        let contacts =
            let contactsUri = ContactsContract.Contacts.ContentUri
            let contactProjection = [|
                    ContactsContract.Contacts.InterfaceConsts.Id;
                    ContactsContract.Contacts.InterfaceConsts.DisplayName
                |]
            let cursor = activity.ManagedQuery (contactsUri, contactProjection, null, null, null)
            let items = getContactsInfo cursor
            items
    
        contacts
