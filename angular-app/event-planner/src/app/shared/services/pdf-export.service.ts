import jsPDF from 'jspdf';
import { DatePipe } from '@angular/common';
import autoTable from 'jspdf-autotable'
import { Injectable } from '@angular/core';

@Injectable()
export class PdfExportService {

  constructor(private datePipe: DatePipe) { }

  exportPdf(source: any, eventName: string): void {
      let PDF = new jsPDF('p', 'mm', 'a4');

      let formatedCurrentDate = this.datePipe.transform(new Date(), "dd/MM/yyyy 'la ora' HH:mm:ss");
      PDF.setFontSize(18);
      PDF.text(`${eventName} - lista de participanti`, 20, 10);
      PDF.text(`Exportat la data de ${formatedCurrentDate}`, 20, 20);
      autoTable(PDF, { html: source, startY: 30 })
      PDF.save(`${eventName.replace(" ", "_")}_participanti.pdf`);
    }
}
