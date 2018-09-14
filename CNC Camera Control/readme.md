## GRBL Camera Control

This control is based on the [AForge libraries](http://www.aforgenet.com/framework/downloads.html).
It should be added to a non-modal window in order to allow jogging and/or MDI-based movement from the main application.

The main application should subscribe to the MoveOffset event and send the appropriate commands to GRBL.

Currently there are three configuration parameters available:

XOffset, Yoffset and Mode. Mode should be set to one of the values defined by the enum MoveMode.

Please note that this version very basic, more functionality will be added later.
