import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable()
export class SearchPeriodValidatorService {
  constructor() {}

  public areSearchDatesValid: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    const startDate = group.get('startDate');
    const endDate = group.get('endDate');
    if (startDate?.value != null && endDate?.value == null) {
      endDate?.setErrors({ endDateIsRequired: true });
      return { endDateIsRequired: true };
    }
    if (startDate?.value == null && endDate?.value != null) {
      startDate?.setErrors({ startDateIsRequired: true });
      return { startDateIsRequired: true };
    }
    if (endDate?.errors == null && startDate?.errors == null) {
      return null;
    }
    return { ...endDate?.errors, ...startDate?.errors };
  };

  getStartDateErrorMessage(form: AbstractControl): string {
    var errors = form.get('startDate')?.errors;
    if (errors == null) return '';
    if (errors['startDateIsRequired'] === true) {
      return 'Va rugam selectati data de incepere.';
    }
    return '';
  }

  getEndDateErrorMessage(form: AbstractControl): string {
    var errors = form.get('endDate')?.errors;
    if (errors == null) return '';
    if (errors['endDateIsRequired'] === true) {
      return 'Va rugam selectati data de incheiere.';
    }
    return '';
  }
}
