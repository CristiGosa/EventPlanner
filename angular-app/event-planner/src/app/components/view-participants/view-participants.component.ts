import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-view-participants',
  templateUrl: './view-participants.component.html',
  styleUrls: ['./view-participants.component.css']
})
export class ViewParticipantsComponent {
  columnsToDisplay: string[] = ["name", "email"];

  constructor(
    @Inject(MAT_DIALOG_DATA) public dataSource: any,
    public dialogRef: MatDialogRef<ViewParticipantsComponent>,
  ) {}

  closeDialog(){
    this.dialogRef.close();
  }
}
