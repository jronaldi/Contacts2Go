namespace Contacts2Go

open Android.App
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open Android.Graphics
open Android.Provider
open Android.Content
open Javax.Xml.Datatype

module Contacts =

    let getContactPhones (activity:Activity) (id:string) : string[] = 
        let phonesUri = Contacts.Phones.ContentUri
        let phonesProjection = [|
            Contacts.Phones.InterfaceConsts.Id;
            Contacts.Phones.InterfaceConsts.Number;
        |]
        let cursorPhones = activity.ManagedQuery (phonesUri, phonesProjection, 
            null,//ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + " = " + id,
            null, null)
        [ while cursorPhones.MoveToNext() do yield (cursorPhones.GetString(1)) ]
        |> Array.ofList

    let formatDateTime (refDateInt:int64) =
            let refDate = new Java.Util.Date(refDateInt)
            refDate.ToString()

    let getContactsInfo (activity:Activity) (cur:Android.Database.ICursor) : (string * string * string * string * string[])[] =
        [ while cur.MoveToNext() 
            do yield (
                cur.GetString(0), 
                cur.GetString(1), 
                formatDateTime (cur.GetLong(2)), 
                cur.GetString(3), 
                getContactPhones activity (cur.GetString(0))) ]
        |> Array.ofList

    let GetContactCursor (activity:Activity) =
        let contacts =
            let contactsUri = ContactsContract.Contacts.ContentUri
            let contactProjection = [|
                    ContactsContract.Contacts.InterfaceConsts.Id;
                    ContactsContract.Contacts.InterfaceConsts.DisplayName;
                    ContactsContract.Contacts.InterfaceConsts.ContactLastUpdatedTimestamp;
                    ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber;
                |]
            let cursor = activity.ManagedQuery (contactsUri, contactProjection, null, null, null)
            let items = getContactsInfo activity cursor
            items
              

        contacts
