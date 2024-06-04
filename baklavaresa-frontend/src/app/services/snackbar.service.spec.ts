import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarService } from './snackbar.service';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('SnackbarService', () => {
  let service: SnackbarService;
  let snackBar: MatSnackBar;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [MatSnackBarModule, NoopAnimationsModule],
      providers: [SnackbarService]
    });
    service = TestBed.inject(SnackbarService);
    snackBar = TestBed.inject(MatSnackBar);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should show a snackbar with the given content, type, and action', () => {
    const openSpy = spyOn(snackBar, 'open').and.callThrough();
    const content = 'Snackbar content';
    const type = 'success';
    const action = 'Close';

    service.showSnackbar(content, type, action);

    expect(openSpy).toHaveBeenCalledWith(
      content,
      action,
      jasmine.objectContaining({
        duration: 10000,
        panelClass: ['snackbar-success'],
        verticalPosition: 'top',
        horizontalPosition: 'right',
      })
    );
  });

    it('should dismiss the snackbar when the action button is clicked', () => {
        const openSpy = spyOn(snackBar, 'open').and.callThrough();
        const dismissSpy = spyOn(snackBar, 'dismiss').and.callThrough();
        const content = 'Snackbar content';
        const type = 'success';
        const action = 'Close';
    
        service.showSnackbar(content, type, action);
    
        const snackBarRef = openSpy.calls.mostRecent().returnValue;
        snackBarRef.onAction().subscribe(() => {
        expect(dismissSpy).toHaveBeenCalled();
        });
    });

    it('should dismiss the snackbar when the mouse leaves the snackbar', () => {
        const openSpy = spyOn(snackBar, 'open').and.callThrough();
        const dismissSpy = spyOn(snackBar, 'dismiss').and.callThrough();
        const content = 'Snackbar content';
        const type = 'success';
        const action = 'Close';
    
        service.showSnackbar(content, type, action);
    
        const snackBarRef = openSpy.calls.mostRecent().returnValue;
        snackBarRef.onAction().subscribe(() => {
        expect(dismissSpy).toHaveBeenCalled();
        });
    });

    it('should not dismiss the snackbar when the mouse enters the snackbar', () => {
        const openSpy = spyOn(snackBar, 'open').and.callThrough();
        const dismissSpy = spyOn(snackBar, 'dismiss').and.callThrough();
        const content = 'Snackbar content';
        const type = 'success';
        const action = 'Close';
    
        service.showSnackbar(content, type, action);
    
        const snackBarRef = openSpy.calls.mostRecent().returnValue;
        snackBarRef.onAction().subscribe(() => {
        expect(dismissSpy).not.toHaveBeenCalled();
        });
    });

    it('should not dismiss the snackbar when the mouse enters the action button', () => {
        const openSpy = spyOn(snackBar, 'open').and.callThrough();
        const dismissSpy = spyOn(snackBar, 'dismiss').and.callThrough();
        const content = 'Snackbar content';
        const type = 'success';
        const action = 'Close';
    
        service.showSnackbar(content, type, action);
    
        const snackBarRef = openSpy.calls.mostRecent().returnValue;
        snackBarRef.onAction().subscribe(() => {
        expect(dismissSpy).not.toHaveBeenCalled();
        });
    });

    it('should not dismiss the snackbar when the mouse leaves the snackbar and enters the action button', () => {
        const openSpy = spyOn(snackBar, 'open').and.callThrough();
        const dismissSpy = spyOn(snackBar, 'dismiss').and.callThrough();
        const content = 'Snackbar content';
        const type = 'success';
        const action = 'Close';
    
        service.showSnackbar(content, type, action);
    
        const snackBarRef = openSpy.calls.mostRecent().returnValue;
        snackBarRef.onAction().subscribe(() => {
        expect(dismissSpy).not.toHaveBeenCalled();
        });
    });
    
});
