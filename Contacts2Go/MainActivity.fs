namespace Contacts2Go

open System
open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open Android.Graphics
open Android.Text.Method
open Contacts

type Resources = Contacts2Go.Resource

[<Activity (Label = "Contacts2Go", MainLauncher = true, Icon = "@mipmap/icon")>]
type MainActivity () =
    inherit Activity ()

    let mutable count:int = 1

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resources.Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        let button = this.FindViewById<Button>(Resources.Id.myButton)
        let trace = this.FindViewById<TextView>(Resources.Id.trace)
        trace.set_MovementMethod(new ScrollingMovementMethod());

        button.Click.Add (fun args -> 
            button.Text <- sprintf "%d clicks!" count
            count <- count + 1

            let traceMsg = ""
            // let contactDisplayList = Array.collect (fun (id,name) -> [|sprintf "%s = %s\n" id name|]) (GetContactCursor this)
            let contactDisplayList = Array.fold (fun msg (id,name) -> msg + (sprintf "%s = %s\n" id name)) "" (GetContactCursor this)
            trace.SetText(contactDisplayList, TextView.BufferType.Normal)
            ()
        )

                
