import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PdfExportService } from 'src/app/shared/services/pdf-export.service';

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
    private pdfExportService: PdfExportService,
  ) {}

  closeDialog(){
    this.dialogRef.close();
  }

  exportPdf(){
    const source = document.getElementById("participants-table");
    this.pdfExportService.exportPdf(source, this.dataSource.eventName);
  }
}
