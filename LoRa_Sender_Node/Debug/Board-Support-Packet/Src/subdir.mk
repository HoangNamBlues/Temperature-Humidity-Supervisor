################################################################################
# Automatically-generated file. Do not edit!
# Toolchain: GNU Tools for STM32 (11.3.rel1)
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../Board-Support-Packet/Src/DHT22.c \
../Board-Support-Packet/Src/LCD_I2C.c \
../Board-Support-Packet/Src/LoRa.c 

OBJS += \
./Board-Support-Packet/Src/DHT22.o \
./Board-Support-Packet/Src/LCD_I2C.o \
./Board-Support-Packet/Src/LoRa.o 

C_DEPS += \
./Board-Support-Packet/Src/DHT22.d \
./Board-Support-Packet/Src/LCD_I2C.d \
./Board-Support-Packet/Src/LoRa.d 


# Each subdirectory must supply rules for building sources it contributes
Board-Support-Packet/Src/%.o Board-Support-Packet/Src/%.su Board-Support-Packet/Src/%.cyclo: ../Board-Support-Packet/Src/%.c Board-Support-Packet/Src/subdir.mk
	arm-none-eabi-gcc "$<" -mcpu=cortex-m3 -std=gnu11 -g3 -DDEBUG -DUSE_HAL_DRIVER -DSTM32F103xB -c -I"D:/My-Projects/Git/Thesis-codes/LoRa_Sender_Node/Board-Support-Packet/Inc" -I../Core/Inc -I../Drivers/STM32F1xx_HAL_Driver/Inc -I../Drivers/STM32F1xx_HAL_Driver/Inc/Legacy -I../Drivers/CMSIS/Device/ST/STM32F1xx/Include -I../Drivers/CMSIS/Include -O0 -ffunction-sections -fdata-sections -Wall -fstack-usage -fcyclomatic-complexity -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" --specs=nano.specs -mfloat-abi=soft -mthumb -o "$@"

clean: clean-Board-2d-Support-2d-Packet-2f-Src

clean-Board-2d-Support-2d-Packet-2f-Src:
	-$(RM) ./Board-Support-Packet/Src/DHT22.cyclo ./Board-Support-Packet/Src/DHT22.d ./Board-Support-Packet/Src/DHT22.o ./Board-Support-Packet/Src/DHT22.su ./Board-Support-Packet/Src/LCD_I2C.cyclo ./Board-Support-Packet/Src/LCD_I2C.d ./Board-Support-Packet/Src/LCD_I2C.o ./Board-Support-Packet/Src/LCD_I2C.su ./Board-Support-Packet/Src/LoRa.cyclo ./Board-Support-Packet/Src/LoRa.d ./Board-Support-Packet/Src/LoRa.o ./Board-Support-Packet/Src/LoRa.su

.PHONY: clean-Board-2d-Support-2d-Packet-2f-Src

