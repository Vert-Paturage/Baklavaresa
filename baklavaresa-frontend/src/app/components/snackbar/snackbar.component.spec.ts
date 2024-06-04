import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SnackbarComponent } from './snackbar.component';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';

describe('SnackbarComponent', () => {
  let component: SnackbarComponent;
  let fixture: ComponentFixture<SnackbarComponent>;
  let snackBarRefMock: jasmine.SpyObj<MatSnackBarRef<SnackbarComponent>>;

  beforeEach(async () => {
    snackBarRefMock = jasmine.createSpyObj('MatSnackBarRef', ['dismiss']);

    await TestBed.configureTestingModule({
      declarations: [ SnackbarComponent ],
      imports: [ NoopAnimationsModule, MatButtonModule ],
      providers: [
        { provide: MatSnackBarRef, useValue: snackBarRefMock },
        { provide: MAT_SNACK_BAR_DATA, useValue: 'Test data' }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SnackbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the data', () => {
    const dataElement = fixture.debugElement.query(By.css('div')).nativeElement;
    expect(dataElement.textContent).toContain('Test data');
  });

  it('should call dismiss method when dismiss button is clicked', () => {
    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    button.click();
    expect(snackBarRefMock.dismiss).toHaveBeenCalled();
  });

    it('should not dismiss the snackbar when the mouse leaves the snackbar', () => {
        const snackbar = fixture.debugElement.query(By.css('div')).nativeElement;
        snackbar.dispatchEvent(new Event('mouseleave'));
        expect(snackBarRefMock.dismiss).not.toHaveBeenCalled();
    });

    it('should not dismiss the snackbar when the mouse enters the dismiss button', () => {
        const button = fixture.debugElement.query(By.css('button')).nativeElement;
        button.dispatchEvent(new Event('mouseenter'));
        expect(snackBarRefMock.dismiss).not.toHaveBeenCalled();
    });

    it('should not dismiss the snackbar when the mouse leaves the snackbar and enters the dismiss button', () => {
        const snackbar = fixture.debugElement.query(By.css('div')).nativeElement;
        snackbar.dispatchEvent(new Event('mouseleave'));
        const button = fixture.debugElement.query(By.css('button')).nativeElement;
        button.dispatchEvent(new Event('mouseenter'));
        expect(snackBarRefMock.dismiss).not.toHaveBeenCalled();
    });
});
