# HammingCodes
Hamming code is a set of error-correction codes that can be used to detect and correct the errors that can occur when the data is moved or stored from the sender to the receiver.

# How to use

To perform operations of encoding and decoding information you need to create the HammingCode class using one of the standard constructors or a special constructor:

```C#
HammingCode h = Hamming.Create24X16(); // 16 bits of information and 24 bits in a codeword
``` 

## Information block

Information (set of bits) must be submitted as an integer of type 'long':

```C#
long information = 0xABCD;
``` 

## Getting Hamming code for information block

To get the parity bits you need to call the Encode method:

```C#
long parity = h.Encode(information);
``` 

## Concatenation of parity bits and information bits

The code word is obtained by attaching parity bits to the information word:

```C#
long codeword = information + (parity << h.InformationBitsNumber);
``` 

## Correcting a corrupted code block

To get the initial information you need to call the Decode method:

```C#
long codewordWithError = codeword ^ 4;
long? decoded = h.Decode(codewordWithError) & h.InformationBitsMask;
``` 

If the returned value is null then the information cannot be recovered.
