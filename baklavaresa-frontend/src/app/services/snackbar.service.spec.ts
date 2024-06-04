import { TestBed } from '@angular/core/testing';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarService } from './snackbar.service';

describe('SnackbarService', () => {
    let service: SnackbarService;
    let snackBarSpy: jasmine.SpyObj<MatSnackBar>;

    beforeEach(() => {
        const spy = jasmine.createSpyObj('MatSnackBar', ['open']);

        TestBed.configureTestingModule({
            imports: [MatSnackBarModule],
            providers: [
                SnackbarService,
                { provide: MatSnackBar, useValue: spy }
            ]
        });

        service = TestBed.inject(SnackbarService);
        snackBarSpy = TestBed.inject(MatSnackBar) as jasmine.SpyObj<MatSnackBar>;
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should show a snackbar with correct configuration', () => {
        const message = 'Test message';
        const type = 'success';
        const action = 'Fermer';

        const snackBarRefSpy = jasmine.createSpyObj('MatSnackBarRef', ['onAction', 'dismiss']);
        snackBarRefSpy.onAction.and.returnValue({
            subscribe: (callback: () => void) => callback()
        });

        snackBarSpy.open.and.returnValue(snackBarRefSpy);

        service.showSnackbar(message, type, action);

        expect(snackBarSpy.open).toHaveBeenCalledWith(
            message,
            action,
            {
                duration: 10000,
                panelClass: ['snackbar-success'],
                verticalPosition: 'top',
                horizontalPosition: 'right',
            }
        );

        expect(snackBarRefSpy.onAction).toHaveBeenCalled();
        expect(snackBarRefSpy.dismiss).toHaveBeenCalled();
    });

    it('should show a snackbar with default action', () => {
        const message = 'Test message';
        const type = 'success';

        const snackBarRefSpy = jasmine.createSpyObj('MatSnackBarRef', ['onAction', 'dismiss']);
        snackBarRefSpy.onAction.and.returnValue({
            subscribe: (callback: () => void) => callback()
        });

        snackBarSpy.open.and.returnValue(snackBarRefSpy);

        service.showSnackbar(message, type);

        expect(snackBarSpy.open).toHaveBeenCalledWith(
            message,
            'Fermer',
            {
                duration: 10000,
                panelClass: ['snackbar-success'],
                verticalPosition: 'top',
                horizontalPosition: 'right',
            }
        );

        expect(snackBarRefSpy.onAction).toHaveBeenCalled();

        expect(snackBarRefSpy.dismiss).toHaveBeenCalled();

    });

    it('should show a snackbar with error type', () => {
        const message = 'Test message';
        const type = 'error';

        const snackBarRefSpy = jasmine.createSpyObj('MatSnackBarRef', ['onAction', 'dismiss']);
        snackBarRefSpy.onAction.and.returnValue({
            subscribe: (callback: () => void) => callback()
        });

        snackBarSpy.open.and.returnValue(snackBarRefSpy);

        service.showSnackbar(message, type);

        expect(snackBarSpy.open).toHaveBeenCalledWith(
            message,
            'Fermer',
            {
                duration: 10000,
                panelClass: ['snackbar-error'],
                verticalPosition: 'top',
                horizontalPosition: 'right',

            }
        );

        expect(snackBarRefSpy.onAction).toHaveBeenCalled();
        expect(snackBarRefSpy.dismiss).toHaveBeenCalled();
    });
});
