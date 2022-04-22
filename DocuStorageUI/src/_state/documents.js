import { atom } from 'recoil';

const documentsAtom = atom({
    key: 'documents',
    default: null,
    writable: true
});

export { documentsAtom };