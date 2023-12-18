import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrls: ['./error-message.component.css']
})
export class ErrorMessageComponent implements OnChanges {

  public statusDictionary: { [id: number] : string; } = {
    0: "No Connection",
    500: "Internal Server Error",
  }

  public templatedErrorCodeDictionary: { [id: string] : (messageDetails: any) => string; } = {
    "NoEntityFound": (entityName) => "No entity with the name" + entityName + '.',

    "NoContent": (entityName) => "No content for" + entityName + '.',

    "NotFoundUser": (userEmail) => "No user found for" + '"' + userEmail + '".'
  };

  @Input() errorResponse: HttpErrorResponse | null;

  public errorMessage : string = "";

  public hasErrorMessage() : boolean {
    return this.errorResponse != null;
  }

  ngOnChanges(changes: SimpleChanges) {
    this.handleErrors();
  }

  public handleErrors() {
    if (this.errorResponse == null) {
      return;
    }

    this.errorMessage = "";

    if (this.errorResponse.status in this.statusDictionary) {
      this.errorMessage = this.statusDictionary[this.errorResponse.status];
      return;
    }

    let errorList = this.errorResponse?.error.errors as Array<any>;
    let isListingOn = errorList.length > 1;

    errorList.forEach(error => {
      switch(error.errorType) {
        case "TemplatedError" :
          this.errorMessage += this.templatedErrorCodeDictionary[error.errorCode](error.messageDetails);
          break;
        default :
          this.errorMessage += error.message;
          break;
      }
      this.errorMessage += isListingOn ? "<br>" : "";
    });
  }
}