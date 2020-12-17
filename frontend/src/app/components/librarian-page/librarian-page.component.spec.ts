import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LibrarianPageComponent } from './librarian-page.component';

describe('LibrarianPageComponent', () => {
  let component: LibrarianPageComponent;
  let fixture: ComponentFixture<LibrarianPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LibrarianPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LibrarianPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
