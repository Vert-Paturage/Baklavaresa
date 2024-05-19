import ContactDetails from './contactDetail.type';
import Table from './table.type';

type Reservation = {
    id: number;
	date: Date;
	peopleNumber: number;
    contactDetails: ContactDetails;
    tables: Table[];
};

export default Reservation;