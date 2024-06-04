import { Injectable } from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private snackBar: MatSnackBar) { }

  showSnackbar(content: string, type: string, action: string = 'Fermer') {
    let style = 'snackbar-' + type;
    console.log(style);
    let sb = this.snackBar.open(content, action, {
      duration: 10000,
      panelClass: [`${style}`],
      verticalPosition: 'top',
      horizontalPosition: 'right',
    });
    sb.onAction().subscribe(() => {
      sb.dismiss();
    });
  }
}