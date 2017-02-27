import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CodexCategoryComponent} from './codexCategory.component';

describe('CodexCategoryComponent', () => {
	let component: CodexCategoryComponent;
	let fixture: ComponentFixture<CodexCategoryComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [CodexCategoryComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(CodexCategoryComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
